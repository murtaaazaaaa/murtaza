using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VelvetLeashAPI.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

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

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

        [ForeignKey("PetSitterId")]
        public virtual PetSitter PetSitter { get; set; } = null!;

        [ForeignKey("PetId")]
        public virtual Pet Pet { get; set; } = null!;

        public virtual ICollection<BookingMessage> Messages { get; set; } = new List<BookingMessage>();
    }

    public enum ServiceType
    {
        Boarding = 1,
        DayCare = 2,
        Walking = 3,
        Sitting = 4,
        Grooming = 5
    }

    public enum BookingStatus
    {
        Pending = 1,
        Confirmed = 2,
        InProgress = 3,
        Completed = 4,
        Cancelled = 5,
        Declined = 6
    }
}