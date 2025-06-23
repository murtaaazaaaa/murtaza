using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VelvetLeashAPI.DTOs;
using VelvetLeashAPI.Services;

namespace VelvetLeashAPI.Controllers.Pet
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetsController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpPost]
        public async Task<ActionResult<PetDto>> CreatePet([FromBody] CreatePetDto createPetDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var result = await _petService.CreatePetAsync(userId, createPetDto);
                return CreatedAtAction(nameof(GetPet), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the pet", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PetDto>> GetPet(int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var result = await _petService.GetPetByIdAsync(id, userId);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the pet", details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<PetDto>>> GetUserPets()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var result = await _petService.GetUserPetsAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving pets", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PetDto>> UpdatePet(int id, [FromBody] UpdatePetDto updatePetDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var result = await _petService.UpdatePetAsync(id, userId, updatePetDto);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the pet", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePet(int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var result = await _petService.DeletePetAsync(id, userId);
                if (!result)
                {
                    return NotFound(new { message = "Pet not found" });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the pet", details = ex.Message });
            }
        }
    }
}