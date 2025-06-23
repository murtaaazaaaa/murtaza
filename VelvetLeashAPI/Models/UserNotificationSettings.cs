using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VelvetLeashAPI.Models
{
    public class UserNotificationSettings
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        // Email Settings
        public bool EmailNotifications { get; set; } = true;
        public bool MarketingEmails { get; set; } = false;

        // SMS Settings
        public bool SmsNotifications { get; set; } = true;
        public bool MessageNotifications { get; set; } = true;
        public bool NewInquiries { get; set; } = true;
        public bool NewMessages { get; set; } = true;
        public bool BookingRequests { get; set; } = true;
        public bool BookingDeclined { get; set; } = true;
        public bool MmsSupport { get; set; } = false;
        public bool QuietHours { get; set; } = false;
        public bool MarketingSms { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;
    }
}