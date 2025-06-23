using System.ComponentModel.DataAnnotations;

namespace VelvetLeashAPI.DTOs
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [MaxLength(10)]
        public string? ZipCode { get; set; }

        [MaxLength(500)]
        public string? HowDidYouHear { get; set; }
    }

    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }

    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public UserDto User { get; set; } = null!;
    }

    public class UserDto
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? ZipCode { get; set; }
        public string? HowDidYouHear { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class SocialLoginDto
    {
        [Required]
        public string Provider { get; set; } = string.Empty; // "facebook" or "google"

        [Required]
        public string Token { get; set; } = string.Empty;

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}