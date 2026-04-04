using System.ComponentModel.DataAnnotations;

namespace BTS.Models
{
    public class BusType
    {
        [Key]
        public string? BusTypeId { get; set; } 
        [Required]
        public string? BusTypeName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
