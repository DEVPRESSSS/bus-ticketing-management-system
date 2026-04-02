using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.Models
{
    public class Seats
    {
        [Key]
        public string SeatId { get; set; } = $"SEAT-{Guid.NewGuid().ToString().Substring(0, 5)}";
        [Required]
        public string? BusId { get; set; }
        [ForeignKey(nameof(BusId))]
        [ValidateNever]
        public Buses? Buses { get; set; }
        [Required]
        public string? SeatNumber { get; set; }
        public string? SeatClass { get; set; }
        public bool isActive { get; set; }
        public DateTime CreatedBy { get; set; } = DateTime.Now;
    }
}
