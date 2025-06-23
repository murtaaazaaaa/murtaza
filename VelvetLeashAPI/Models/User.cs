using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VelvetLeashAPI.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(10)]
        public string? ZipCode { get; set; }

        [MaxLength(500)]
        public string? HowDidYouHear { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual ICollection<Review> ReviewsGiven { get; set; } = new List<Review>();
        public virtual ICollection<Review> ReviewsReceived { get; set; } = new List<Review>();
        public virtual PetSitter? PetSitter { get; set; }
        public virtual UserNotificationSettings? NotificationSettings { get; set; }
    }
}