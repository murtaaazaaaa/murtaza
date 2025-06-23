using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VelvetLeashAPI.DTOs;
using VelvetLeashAPI.Models;
using VelvetLeashAPI.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace VelvetLeashAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly VelvetLeashDbContext _context;
        private readonly IMapper _mapper;

        public AuthService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration,
            VelvetLeashDbContext context,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists");
            }

            var user = new User
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                ZipCode = registerDto.ZipCode,
                HowDidYouHear = registerDto.HowDidYouHear,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Failed to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            // Create default notification settings
            var notificationSettings = new UserNotificationSettings
            {
                UserId = user.Id,
                EmailNotifications = true,
                SmsNotifications = true,
                MessageNotifications = true,
                NewInquiries = true,
                NewMessages = true,
                BookingRequests = true,
                BookingDeclined = true
            };

            _context.UserNotificationSettings.Add(notificationSettings);
            await _context.SaveChangesAsync();

            var token = await GenerateJwtTokenAsync(user.Id);
            var userDto = _mapper.Map<UserDto>(user);

            return new AuthResponseDto
            {
                Token = token,
                RefreshToken = Guid.NewGuid().ToString(),
                Expiration = DateTime.UtcNow.AddDays(7),
                User = userDto
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            var token = await GenerateJwtTokenAsync(user.Id);
            var userDto = _mapper.Map<UserDto>(user);

            return new AuthResponseDto
            {
                Token = token,
                RefreshToken = Guid.NewGuid().ToString(),
                Expiration = DateTime.UtcNow.AddDays(7),
                User = userDto
            };
        }

        public async Task<AuthResponseDto> SocialLoginAsync(SocialLoginDto socialLoginDto)
        {
            // In a real implementation, you would validate the social token with the provider
            // For now, we'll create/find user based on email
            
            if (string.IsNullOrEmpty(socialLoginDto.Email))
            {
                throw new InvalidOperationException("Email is required for social login");
            }

            var user = await _userManager.FindByEmailAsync(socialLoginDto.Email);
            
            if (user == null)
            {
                // Create new user
                user = new User
                {
                    UserName = socialLoginDto.Email,
                    Email = socialLoginDto.Email,
                    FirstName = socialLoginDto.FirstName ?? "",
                    LastName = socialLoginDto.LastName ?? "",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    EmailConfirmed = true // Social logins are pre-verified
                };

                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException($"Failed to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }

                // Create default notification settings
                var notificationSettings = new UserNotificationSettings
                {
                    UserId = user.Id,
                    EmailNotifications = true,
                    SmsNotifications = true,
                    MessageNotifications = true,
                    NewInquiries = true,
                    NewMessages = true,
                    BookingRequests = true,
                    BookingDeclined = true
                };

                _context.UserNotificationSettings.Add(notificationSettings);
                await _context.SaveChangesAsync();
            }

            var token = await GenerateJwtTokenAsync(user.Id);
            var userDto = _mapper.Map<UserDto>(user);

            return new AuthResponseDto
            {
                Token = token,
                RefreshToken = Guid.NewGuid().ToString(),
                Expiration = DateTime.UtcNow.AddDays(7),
                User = userDto
            };
        }

        public async Task<string> GenerateJwtTokenAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? ""));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "");

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task LogoutAsync(string userId)
        {
            // In a real implementation, you might want to blacklist the token
            // For now, we'll just sign out the user
            await _signInManager.SignOutAsync();
        }
    }
}