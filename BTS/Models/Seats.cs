using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.Models
{
    public class Seats
    {
        [Key]
        public string? SeatId { get; set; } 
        [Required]
        public string? BusId { get; set; }
        [ForeignKey(nameof(BusId))]
        public Buses? Buses { get; set; }
        [Required]
        [Display(Name ="Seat")]
        public string? SeatNumber { get; set; }
        public string? SeatClass { get; set; }
        public bool isActive { get; set; } = true;
        public DateTime CreatedBy { get; set; } = DateTime.Now;
    }
}
