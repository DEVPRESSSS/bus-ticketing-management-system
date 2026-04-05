using BTS.Areas.Service.SD;
using BTS.Data;
using BTS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BTS.Areas.Service.Initializer
{
    public class DbInitializer : IDbinitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Initialize()
        {
            if (_context.Database.GetPendingMigrations().Any())
            {
                _context.Database.Migrate();
            }
            if (!_roleManager.RoleExistsAsync(SdRoles.Customer).GetAwaiter().GetResult() || !_roleManager.RoleExistsAsync(SdRoles.Customer).GetAwaiter().GetResult())
            {
                // Create Roles
                _roleManager.CreateAsync(new IdentityRole(SdRoles.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SdRoles.Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SdRoles.Customer)).GetAwaiter().GetResult();

                // Create Admin User
                var user = new ApplicationUser
                {
                    UserName = "xmontemorjerald@gmail.com",
                    Email = "xmontemorjerald@gmail.com",
                    PhoneNumber = "09789449801",
                    EmailConfirmed = true
                };

                _userManager.CreateAsync(user, "Admin123*").GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(user, SdRoles.Admin).GetAwaiter().GetResult();
            }


        }
    }
}
