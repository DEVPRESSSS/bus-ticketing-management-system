using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.Models
{
    public class BusRoutes
    {
        [Key]
        public string? RouteId { get; set; }

        // Origin Station
        [Required]
        public string? StationId { get; set; }
        [ForeignKey(nameof(StationId))]
        public Stations? OriginStation { get; set; }

        [Required]
        public string? DestinationId { get; set; }
        [ForeignKey(nameof(DestinationId))]
        public Stations? DestinationStation { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal? BasePrice { get; set; }
        [Required]
        [Precision(10, 2)]
        public decimal? DistanceKM { get; set; }
  
        [Required]
        public int? EstimatedTime { get; set; }
        public bool? IsActive { get; set; } = false;
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}