using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VelvetLeashAPI.Data;
using VelvetLeashAPI.DTOs;
using VelvetLeashAPI.Models;

namespace VelvetLeashAPI.Services
{
    public class PetService : IPetService
    {
        private readonly VelvetLeashDbContext _context;
        private readonly IMapper _mapper;

        public PetService(VelvetLeashDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PetDto> CreatePetAsync(string userId, CreatePetDto createPetDto)
        {
            var pet = _mapper.Map<Pet>(createPetDto);
            pet.UserId = userId;
            pet.CreatedAt = DateTime.UtcNow;
            pet.UpdatedAt = DateTime.UtcNow;

            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();

            return _mapper.Map<PetDto>(pet);
        }

        public async Task<PetDto> GetPetByIdAsync(int petId, string userId)
        {
            var pet = await _context.Pets
                .FirstOrDefaultAsync(p => p.Id == petId && p.UserId == userId);

            if (pet == null)
            {
                throw new KeyNotFoundException("Pet not found");
            }

            return _mapper.Map<PetDto>(pet);
        }

        public async Task<List<PetDto>> GetUserPetsAsync(string userId)
        {
            var pets = await _context.Pets
                .Where(p => p.UserId == userId)
                .OrderBy(p => p.Name)
                .ToListAsync();

            return _mapper.Map<List<PetDto>>(pets);
        }

        public async Task<PetDto> UpdatePetAsync(int petId, string userId, UpdatePetDto updatePetDto)
        {
            var pet = await _context.Pets
                .FirstOrDefaultAsync(p => p.Id == petId && p.UserId == userId);

            if (pet == null)
            {
                throw new KeyNotFoundException("Pet not found");
            }

            // Update only provided fields
            if (!string.IsNullOrEmpty(updatePetDto.Name))
                pet.Name = updatePetDto.Name;
            
            if (updatePetDto.Type.HasValue)
                pet.Type = updatePetDto.Type.Value;
            
            if (updatePetDto.Size.HasValue)
                pet.Size = updatePetDto.Size.Value;
            
            if (updatePetDto.Age.HasValue)
                pet.Age = updatePetDto.Age.Value;
            
            if (updatePetDto.GetAlongWithDogs.HasValue)
                pet.GetAlongWithDogs = updatePetDto.GetAlongWithDogs.Value;
            
            if (updatePetDto.GetAlongWithCats.HasValue)
                pet.GetAlongWithCats = updatePetDto.GetAlongWithCats.Value;
            
            if (updatePetDto.IsUnsureWithDogs.HasValue)
                pet.IsUnsureWithDogs = updatePetDto.IsUnsureWithDogs.Value;
            
            if (updatePetDto.IsUnsureWithCats.HasValue)
                pet.IsUnsureWithCats = updatePetDto.IsUnsureWithCats.Value;
            
            if (updatePetDto.SpecialInstructions != null)
                pet.SpecialInstructions = updatePetDto.SpecialInstructions;
            
            if (updatePetDto.MedicalConditions != null)
                pet.MedicalConditions = updatePetDto.MedicalConditions;

            pet.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return _mapper.Map<PetDto>(pet);
        }

        public async Task<bool> DeletePetAsync(int petId, string userId)
        {
            var pet = await _context.Pets
                .FirstOrDefaultAsync(p => p.Id == petId && p.UserId == userId);

            if (pet == null)
            {
                return false;
            }

            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}