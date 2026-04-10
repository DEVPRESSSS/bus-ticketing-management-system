using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BTS.Models
{
    public class ApplicationUser:IdentityUser
    {
        
        [Required]
        [MaxLength(60)]
        public string? FullName { get; set; }
        
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
     

    }
}
