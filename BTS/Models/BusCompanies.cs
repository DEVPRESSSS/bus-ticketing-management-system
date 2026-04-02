using System.ComponentModel.DataAnnotations;

namespace BTS.Models
{
    public class BusCompanies
    {
        [Key]
        public string? BusCompanyId { get; set; } = $"BUSCOMPANY-{Guid.NewGuid().ToString().Substring(0, 5)}";
        [Required]
        public string? CompanyName { get; set; }
        [Required]
        public string? ContactNumber { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Address { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
