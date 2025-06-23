using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VelvetLeashAPI.Models
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }

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

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Foreign key
        [Required]
        public string UserId { get; set; } = string.Empty;

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }

    public enum PetType
    {
        Dog = 1,
        Cat = 2,
        Bird = 3,
        Fish = 4,
        Rabbit = 5,
        Other = 6
    }

    public enum PetSize
    {
        Small = 1,      // 0-15 lbs
        Medium = 2,     // 16-40 lbs
        Large = 3,      // 41-100 lbs
        ExtraLarge = 4  // 101+ lbs
    }

    public enum PetAge
    {
        Puppy = 1,      // Less than 1 year
        Adult = 2       // 1 year or older
    }
}