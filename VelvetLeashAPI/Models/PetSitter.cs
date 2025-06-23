using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VelvetLeashAPI.Models
{
    public class PetSitter
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

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

        public bool IsStarSitter { get; set; } = false;
        public bool IsAvailable { get; set; } = true;

        [Column(TypeName = "decimal(18,2)")]
        public decimal? HourlyRate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? DailyRate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? OvernightRate { get; set; }

        public double AverageRating { get; set; } = 0.0;
        public int TotalReviews { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<PetSitterService> Services { get; set; } = new List<PetSitterService>();
    }
}