using BTS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using System.Net.Sockets;

namespace BTS.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        //public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<Stations> Stations { get; set; }
        public DbSet<BusType> BusType { get; set; }
        public DbSet<BusCompanies> BusCompanies { get; set; }
        public DbSet<Buses> Buses { get; set; }
        public DbSet<Seats> Seats { get; set; }
        public DbSet<BusRoutes> BusRoutes { get; set; }
        public DbSet<Schedules> Schedules { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<IdentityPasskeyData>().HasNoKey();

            #region--Stations
            modelBuilder.Entity<Stations>()
                .HasIndex(s => s.StationName)
                .IsUnique();
            modelBuilder.Entity<Stations>()
                .HasIndex(s => s.Addresss)
                .IsUnique();
            #endregion
            
            #region--Buses
            modelBuilder.Entity<Buses>()
               .HasIndex(s => s.BusNumber)
               .IsUnique();
            modelBuilder.Entity<Buses>()
               .HasIndex(s => s.PlateNumber)
               .IsUnique();
            #endregion

            #region--BusCompany
            modelBuilder.Entity<BusCompanies>()
             .HasIndex(s => s.CompanyName)
             .IsUnique();
            modelBuilder.Entity<BusCompanies>()
                .HasIndex(s => s.ContactNumber)
                .IsUnique();
            modelBuilder.Entity<BusCompanies>()
                .HasIndex(s => s.Email)
                .IsUnique();
            modelBuilder.Entity<BusCompanies>()
               .HasIndex(s => s.Address)
               .IsUnique();
            #endregion

            #region--Routes
            modelBuilder.Entity<BusRoutes>()
                .HasOne(r => r.OriginStation)
                .WithMany(s => s.DepartingRoutes)
                .HasForeignKey(r => r.StationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BusRoutes>()
                .HasOne(r => r.DestinationStation)
                .WithMany(s => s.ArrivingRoutes)
                .HasForeignKey(r => r.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region--ApplicationUser
                modelBuilder.Entity<ApplicationUser>()
                   .HasIndex(s => s.FullName)
                   .IsUnique();
                modelBuilder.Entity<ApplicationUser>()
                    .HasIndex(s => s.Email)
                    .IsUnique();

            #endregion

            #region--Tickets
            modelBuilder.Entity<Tickets>()
               .HasOne(t => t.Seats)
               .WithMany()
               .HasForeignKey(t => t.SeatId)
               .OnDelete(DeleteBehavior.NoAction);  

            modelBuilder.Entity<Tickets>()
                .HasOne(t => t.Schedules)
                .WithMany()
                .HasForeignKey(t => t.ScheduledId)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<Tickets>()
                .HasOne(t => t.ApplicationUser)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region--Seeders
                
                modelBuilder.Entity<BusCompanies>().HasData(
                    new BusCompanies
                    {
                        BusCompanyId = "BUSCOMPANY-07d79", 
                        CompanyName = "BALIWAG",
                        Address = "Manila, Recto",
                        ContactNumber = "09488749263",
                        Email = "incs@gmail.com",
                        CreatedAt = new DateTime(2026, 4, 2, 22, 4, 37)  
                    },
                    new BusCompanies
                    {
                        BusCompanyId = "BUSCOMPANY-37995",  
                        CompanyName = "INC",
                        Address = "Manila, Recto Station",
                        ContactNumber = "09488549263",
                        Email = "inc@gmail.com",
                        CreatedAt = new DateTime(2026, 4, 2, 22, 4, 37) 
                    }
                );


                modelBuilder.Entity<BusType>().HasData(
                      new BusType
                      {
                          BusTypeId = "BUSCOMPANY-07d79",
                          BusTypeName = "AIR CONDITIONED",
                        
                          CreatedAt = new DateTime(2026, 4, 2, 22, 4, 37)
                      },
                      new BusType
                      {
                          BusTypeId = "BUSCOMPANY-07d78",
                          BusTypeName = "ORDINARY/REGULAR",
                          CreatedAt = new DateTime(2026, 4, 2, 22, 4, 37)
                      },
                      new BusType
                      {
                             BusTypeId = "BUSCOMPANY-07d77",
                             BusTypeName = "DELUXE",
                             CreatedAt = new DateTime(2026, 4, 2, 22, 4, 37)
                      }
                );
            #endregion

        }
    }
}
