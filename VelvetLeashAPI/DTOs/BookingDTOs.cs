using System.ComponentModel.DataAnnotations;
using VelvetLeashAPI.Models;

namespace VelvetLeashAPI.DTOs
{
    public class CreateBookingDto
    {
        [Required]
        public int PetSitterId { get; set; }

        [Required]
        public int PetId { get; set; }

        [Required]
        public ServiceType ServiceType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [MaxLength(1000)]
        public string? SpecialInstructions { get; set; }
    }

    public class UpdateBookingDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [MaxLength(1000)]
        public string? SpecialInstructions { get; set; }

        public BookingStatus? Status { get; set; }
    }

    public class BookingDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int PetSitterId { get; set; }
        public int PetId { get; set; }
        public ServiceType ServiceType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? SpecialInstructions { get; set; }
        public decimal TotalAmount { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Related data
        public UserDto? User { get; set; }
        public PetSitterDto? PetSitter { get; set; }
        public PetDto? Pet { get; set; }
    }

    public class BookingSearchDto
    {
        public string? Location { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ServiceType? ServiceType { get; set; }
        public PetType? PetType { get; set; }
        public PetSize? PetSize { get; set; }
        public PetAge? PetAge { get; set; }
        public bool? GetAlongWithDogs { get; set; }
        public bool? GetAlongWithCats { get; set; }
        public decimal? MaxPrice { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? RadiusInMiles { get; set; } = 25;
    }

    public class BookingMessageDto
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string SenderId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserDto? Sender { get; set; }
    }

    public class CreateBookingMessageDto
    {
        [Required]
        public int BookingId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Message { get; set; } = string.Empty;
    }
}