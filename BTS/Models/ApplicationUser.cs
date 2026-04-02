using System.ComponentModel.DataAnnotations;

namespace BTS.Models
{
    public class ApplicationUser
    {
        [Key]
        public string? UserId { get; set; }
        [Required]
        [MaxLength(60)]
        public string? FullName { get; set; }
        [Required]
        [MaxLength(60)]
        public string? Email { get; set; }
        [Required]
        [MaxLength(30)]
        public string? Password { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
