using Microsoft.AspNetCore.Mvc;
using VelvetLeashAPI.DTOs;
using VelvetLeashAPI.Services;

namespace VelvetLeashAPI.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var result = await _authService.RegisterAsync(registerDto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during registration", details = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var result = await _authService.LoginAsync(loginDto);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during login", details = ex.Message });
            }
        }

        [HttpPost("social-login")]
        public async Task<ActionResult<AuthResponseDto>> SocialLogin([FromBody] SocialLoginDto socialLoginDto)
        {
            try
            {
                var result = await _authService.SocialLoginAsync(socialLoginDto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during social login", details = ex.Message });
            }
        }

        [HttpPost("validate-token")]
        public async Task<ActionResult<bool>> ValidateToken([FromBody] string token)
        {
            try
            {
                var isValid = await _authService.ValidateTokenAsync(token);
                return Ok(new { isValid });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during token validation", details = ex.Message });
            }
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrEmpty(userId))
                {
                    await _authService.LogoutAsync(userId);
                }
                return Ok(new { message = "Logged out successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during logout", details = ex.Message });
            }
        }
    }
}