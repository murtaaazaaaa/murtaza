using System.ComponentModel.DataAnnotations;
using VelvetLeashAPI.Models;

namespace VelvetLeashAPI.DTOs
{
    public class CreatePetDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public PetType Type { get; set; }

        [Required]
        public PetSize Size { get; set; }

        [Required]
        public PetAge Age { get; set; }

        public bool GetAlongWithDogs { get; set; }
        public bool GetAlongWithCats { get; set; }
        public bool IsUnsureWithDogs { get; set; }
        public bool IsUnsureWithCats { get; set; }

        [MaxLength(1000)]
        public string? SpecialInstructions { get; set; }

        [MaxLength(500)]
        public string? MedicalConditions { get; set; }
    }

    public class UpdatePetDto
    {
        [MaxLength(100)]
        public string? Name { get; set; }

        public PetType? Type { get; set; }
        public PetSize? Size { get; set; }
        public PetAge? Age { get; set; }
        public bool? GetAlongWithDogs { get; set; }
        public bool? GetAlongWithCats { get; set; }
        public bool? IsUnsureWithDogs { get; set; }
        public bool? IsUnsureWithCats { get; set; }

        [MaxLength(1000)]
        public string? SpecialInstructions { get; set; }

        [MaxLength(500)]
        public string? MedicalConditions { get; set; }
    }

    public class PetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public PetType Type { get; set; }
        public PetSize Size { get; set; }
        public PetAge Age { get; set; }
        public bool GetAlongWithDogs { get; set; }
        public bool GetAlongWithCats { get; set; }
        public bool IsUnsureWithDogs { get; set; }
        public bool IsUnsureWithCats { get; set; }
        public string? SpecialInstructions { get; set; }
        public string? MedicalConditions { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class PetSearchCriteriaDto
    {
        public PetSize? Size { get; set; }
        public PetAge? Age { get; set; }
        public bool? GetAlongWithDogs { get; set; }
        public bool? GetAlongWithCats { get; set; }
    }
}