using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VelvetLeashAPI.Models
{
    public class PetSitterService
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PetSitterId { get; set; }

        [Required]
        public ServiceType ServiceType { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("PetSitterId")]
        public virtual PetSitter PetSitter { get; set; } = null!;
    }
}