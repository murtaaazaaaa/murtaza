using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VelvetLeashAPI.DTOs;

namespace VelvetLeashAPI.Controllers.User
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        [HttpGet("profile")]
        public async Task<ActionResult<UserDto>> GetProfile()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                var firstName = User.FindFirst("firstName")?.Value;
                var lastName = User.FindFirst("lastName")?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                // Placeholder response - in real implementation, you would fetch from database
                var userDto = new UserDto
                {
                    Id = userId,
                    FirstName = firstName ?? "John",
                    LastName = lastName ?? "Doe",
                    Email = email ?? "john.doe@example.com",
                    ZipCode = "12345",
                    HowDidYouHear = "Friend referral",
                    CreatedAt = DateTime.UtcNow.AddDays(-30)
                };

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving user profile", details = ex.Message });
            }
        }

        [HttpPut("profile")]
        public async Task<ActionResult<UserDto>> UpdateProfile([FromBody] UpdateUserDto updateUserDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                // Placeholder response - in real implementation, you would update the database
                var userDto = new UserDto
                {
                    Id = userId,
                    FirstName = updateUserDto.FirstName ?? "John",
                    LastName = updateUserDto.LastName ?? "Doe",
                    Email = updateUserDto.Email ?? "john.doe@example.com",
                    ZipCode = updateUserDto.ZipCode ?? "12345",
                    HowDidYouHear = updateUserDto.HowDidYouHear ?? "Friend referral",
                    CreatedAt = DateTime.UtcNow.AddDays(-30)
                };

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating user profile", details = ex.Message });
            }
        }
    }

    public class UpdateUserDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ZipCode { get; set; }
        public string? HowDidYouHear { get; set; }
    }
}