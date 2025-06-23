using VelvetLeashAPI.DTOs;

namespace VelvetLeashAPI.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto> SocialLoginAsync(SocialLoginDto socialLoginDto);
        Task<string> GenerateJwtTokenAsync(string userId);
        Task<bool> ValidateTokenAsync(string token);
        Task LogoutAsync(string userId);
    }
}