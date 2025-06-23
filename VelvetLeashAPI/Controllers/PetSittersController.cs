using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VelvetLeashAPI.DTOs;
using VelvetLeashAPI.Services;

namespace VelvetLeashAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetSittersController : ControllerBase
    {
        private readonly IPetSitterService _petSitterService;

        public PetSittersController(IPetSitterService petSitterService)
        {
            _petSitterService = petSitterService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PetSitterDto>> CreatePetSitterProfile([FromBody] CreatePetSitterDto createPetSitterDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var result = await _petSitterService.CreatePetSitterProfileAsync(userId, createPetSitterDto);
                return CreatedAtAction(nameof(GetPetSitter), new { id = result.Id }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the pet sitter profile", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PetSitterDto>> GetPetSitter(int id)
        {
            try
            {
                var result = await _petSitterService.GetPetSitterByIdAsync(id);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the pet sitter", details = ex.Message });
            }
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult<PetSitterDto>> GetMyProfile()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var result = await _petSitterService.GetPetSitterByUserIdAsync(userId);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving your profile", details = ex.Message });
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<PetSitterDto>>> SearchPetSitters([FromQuery] BookingSearchDto searchDto)
        {
            try
            {
                var result = await _petSitterService.SearchPetSittersAsync(searchDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while searching for pet sitters", details = ex.Message });
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<PetSitterDto>> UpdatePetSitter([FromBody] UpdatePetSitterDto updatePetSitterDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var result = await _petSitterService.UpdatePetSitterAsync(userId, updatePetSitterDto);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the pet sitter profile", details = ex.Message });
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> DeletePetSitter()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var result = await _petSitterService.DeletePetSitterAsync(userId);
                if (!result)
                {
                    return NotFound(new { message = "Pet sitter profile not found" });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the pet sitter profile", details = ex.Message });
            }
        }

        [HttpPost("reviews")]
        [Authorize]
        public async Task<ActionResult<ReviewDto>> CreateReview([FromBody] CreateReviewDto createReviewDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var result = await _petSitterService.CreateReviewAsync(userId, createReviewDto);
                return CreatedAtAction(nameof(GetPetSitterReviews), new { petSitterId = result.RevieweeId }, result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the review", details = ex.Message });
            }
        }

        [HttpGet("{petSitterId}/reviews")]
        public async Task<ActionResult<List<ReviewDto>>> GetPetSitterReviews(int petSitterId)
        {
            try
            {
                var result = await _petSitterService.GetPetSitterReviewsAsync(petSitterId);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving reviews", details = ex.Message });
            }
        }
    }
}