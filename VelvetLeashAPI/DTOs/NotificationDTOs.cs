using System.ComponentModel.DataAnnotations;

namespace VelvetLeashAPI.DTOs
{
    public class NotificationSettingsDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;

        // Email Settings
        public bool EmailNotifications { get; set; }
        public bool MarketingEmails { get; set; }

        // SMS Settings
        public bool SmsNotifications { get; set; }
        public bool MessageNotifications { get; set; }
        public bool NewInquiries { get; set; }
        public bool NewMessages { get; set; }
        public bool BookingRequests { get; set; }
        public bool BookingDeclined { get; set; }
        public bool MmsSupport { get; set; }
        public bool QuietHours { get; set; }
        public bool MarketingSms { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class UpdateNotificationSettingsDto
    {
        // Email Settings
        public bool? EmailNotifications { get; set; }
        public bool? MarketingEmails { get; set; }

        // SMS Settings
        public bool? SmsNotifications { get; set; }
        public bool? MessageNotifications { get; set; }
        public bool? NewInquiries { get; set; }
        public bool? NewMessages { get; set; }
        public bool? BookingRequests { get; set; }
        public bool? BookingDeclined { get; set; }
        public bool? MmsSupport { get; set; }
        public bool? QuietHours { get; set; }
        public bool? MarketingSms { get; set; }
    }
}