using System.ComponentModel.DataAnnotations;

namespace BTS.Models
{
    public class BusType
    {
        [Key]
        public string BusTypeId { get; set; } = $"BUSTYPE-{Guid.NewGuid().ToString().Substring(0, 5)}";
        [Required]
        public string? BusTypeName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
