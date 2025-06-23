using VelvetLeashAPI.DTOs;

namespace VelvetLeashAPI.Services
{
    public interface IPetService
    {
        Task<PetDto> CreatePetAsync(string userId, CreatePetDto createPetDto);
        Task<PetDto> GetPetByIdAsync(int petId, string userId);
        Task<List<PetDto>> GetUserPetsAsync(string userId);
        Task<PetDto> UpdatePetAsync(int petId, string userId, UpdatePetDto updatePetDto);
        Task<bool> DeletePetAsync(int petId, string userId);
    }
}