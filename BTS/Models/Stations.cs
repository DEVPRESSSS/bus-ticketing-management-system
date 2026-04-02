using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BTS.Models
{
    public class Stations
    {
        [Key]
        public string StationId { get; set; } = $"STATION-{Guid.NewGuid().ToString().Substring(0, 5)}";
        [Required]
        [MaxLength(40)]
        public string? StationName { get; set; }
        [Required]
        [MaxLength(50)]
        public string? City { get; set; }
        [Required]
        public string? Province { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Addresss { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public bool? isActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [ValidateNever]
        public ICollection<BusRoutes>? DepartingRoutes { get; set; }
        [ValidateNever]
        public ICollection<BusRoutes>? ArrivingRoutes { get; set; }
    }
}
