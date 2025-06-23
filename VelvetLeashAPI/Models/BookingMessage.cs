using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VelvetLeashAPI.Models
{
    public class BookingMessage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BookingId { get; set; }

        [Required]
        public string SenderId { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Message { get; set; } = string.Empty;

        public bool IsRead { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("BookingId")]
        public virtual Booking Booking { get; set; } = null!;

        [ForeignKey("SenderId")]
        public virtual User Sender { get; set; } = null!;
    }
}