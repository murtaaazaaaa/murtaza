using VelvetLeashAPI.DTOs;

namespace VelvetLeashAPI.Services
{
    public interface IPetSitterService
    {
        Task<PetSitterDto> CreatePetSitterProfileAsync(string userId, CreatePetSitterDto createPetSitterDto);
        Task<PetSitterDto> GetPetSitterByIdAsync(int petSitterId);
        Task<PetSitterDto> GetPetSitterByUserIdAsync(string userId);
        Task<List<PetSitterDto>> SearchPetSittersAsync(BookingSearchDto searchDto);
        Task<PetSitterDto> UpdatePetSitterAsync(string userId, UpdatePetSitterDto updatePetSitterDto);
        Task<bool> DeletePetSitterAsync(string userId);
        Task<ReviewDto> CreateReviewAsync(string reviewerId, CreateReviewDto createReviewDto);
        Task<List<ReviewDto>> GetPetSitterReviewsAsync(int petSitterId);
    }
}