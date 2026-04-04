using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS.Models
{
    public class Buses
    {
        [Key]
        public string? BusId { get; set; } 
        [Required]
        public string? BusName { get; set; }
        [Required]
        public string? BusNumber { get; set; }
        [Required]
        public string? PlateNumber { get; set; }
        [Required]
        public string? BusTypeId { get; set; }

        [ForeignKey(nameof(BusTypeId))]
        public BusType? BusType { get; set; }
        [Required]
        public int TotalSeats { get; set; }
        [Precision(18, 2)]
        public decimal? PricePerKm { get; set; }
        [Required]
        public string? BusCompanyId { get; set; }
        [ForeignKey(nameof(BusCompanyId))]
        [ValidateNever]
        public BusCompanies? BusCompany { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
