using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.Models
{
    public class Schedules
    {
        [Key]
        public string? ScheduleId { get; set; } 
        [Required]
        public string? RouteId { get; set; }
        [ForeignKey(nameof(RouteId))]
        public BusRoutes? Route { get; set; }
        [Required]
        public string? BusId { get; set; }
        [ForeignKey(nameof(BusId))]
        public Buses? Buses { get; set; }
        [Required]
        public DateTime DepartureTime { get; set; }
        [Required]
        public DateTime ArrivalTime { get; set; }

        public string? Status { get; set; }
        [Required]
        public int? AvailableSeats { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
