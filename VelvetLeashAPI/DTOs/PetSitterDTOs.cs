using System.ComponentModel.DataAnnotations;
using VelvetLeashAPI.Models;

namespace VelvetLeashAPI.DTOs
{
    public class CreatePetSitterDto
    {
        [MaxLength(1000)]
        public string? About { get; set; }

        [MaxLength(500)]
        public string? Skills { get; set; }

        [MaxLength(500)]
        public string? HomeDetails { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(50)]
        public string? State { get; set; }

        [MaxLength(10)]
        public string? ZipCode { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public decimal? HourlyRate { get; set; }
        public decimal? DailyRate { get; set; }
        public decimal? OvernightRate { get; set; }

        public List<CreatePetSitterServiceDto>? Services { get; set; }
    }

    public class UpdatePetSitterDto
    {
        [MaxLength(1000)]
        public string? About { get; set; }

        [MaxLength(500)]
        public string? Skills { get; set; }

        [MaxLength(500)]
        public string? HomeDetails { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(50)]
        public string? State { get; set; }

        [MaxLength(10)]
        public string? ZipCode { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public bool? IsAvailable { get; set; }

        public decimal? HourlyRate { get; set; }
        public decimal? DailyRate { get; set; }
        public decimal? OvernightRate { get; set; }
    }

    public class PetSitterDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string? About { get; set; }
        public string? Skills { get; set; }
        public string? HomeDetails { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool IsStarSitter { get; set; }
        public bool IsAvailable { get; set; }
        public decimal? HourlyRate { get; set; }
        public decimal? DailyRate { get; set; }
        public decimal? OvernightRate { get; set; }
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Related data
        public UserDto? User { get; set; }
        public List<PetSitterServiceDto>? Services { get; set; }
        public List<ReviewDto>? Reviews { get; set; }
        public double? DistanceInMiles { get; set; }
    }

    public class CreatePetSitterServiceDto
    {
        [Required]
        public ServiceType ServiceType { get; set; }

        [Required]
        public decimal Price { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }
    }

    public class PetSitterServiceDto
    {
        public int Id { get; set; }
        public int PetSitterId { get; set; }
        public ServiceType ServiceType { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ReviewDto
    {
        public int Id { get; set; }
        public string ReviewerId { get; set; } = string.Empty;
        public string RevieweeId { get; set; } = string.Empty;
        public int BookingId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserDto? Reviewer { get; set; }
    }

    public class CreateReviewDto
    {
        [Required]
        public int BookingId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(1000)]
        public string? Comment { get; set; }
    }
}