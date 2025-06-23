using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VelvetLeashAPI.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ReviewerId { get; set; } = string.Empty;

        [Required]
        public string RevieweeId { get; set; } = string.Empty;

        [Required]
        public int BookingId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(1000)]
        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("ReviewerId")]
        public virtual User Reviewer { get; set; } = null!;

        [ForeignKey("RevieweeId")]
        public virtual User Reviewee { get; set; } = null!;

        [ForeignKey("BookingId")]
        public virtual Booking Booking { get; set; } = null!;
    }
}