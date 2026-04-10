using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.Models
{
    public class Tickets
    {
        [Key]
        public string? TicketId { get; set; } 
        [Required]
        public string TicketCode { get; set; } = $"TCKT-{Guid.NewGuid().ToString().Substring(0, 6)}";

        [Required]
        public string? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser? ApplicationUser { get; set; }

        [Required]
        public string? SeatId { get; set; }
        [ForeignKey(nameof(SeatId))]
        public Seats? Seats { get; set; }

        [Required]
        public string? ScheduledId { get; set; }
        [ForeignKey(nameof(ScheduledId))]
        public Schedules? Schedules { get; set; }

        [Required]
        [Precision(10, 2)]
        public decimal AmountPaid { get; set; }
        public string? Status { get; set; }
        public DateTime? BookedAt { get; set; } = DateTime.Now;
        public DateTime? CancelledAt { get; set; } = DateTime.Now;
    }
}
