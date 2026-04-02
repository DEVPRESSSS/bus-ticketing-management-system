using BTS.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace BTS.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        DbSet<ApplicationUser> Users { get; set; }

        DbSet<Stations> Stations { get; set; }
        DbSet<BusType> BusType { get; set; }
        DbSet<BusCompanies> BusCompanies { get; set; }
        DbSet<Buses> Buses { get; set; }
        DbSet<Seats> Seats { get; set; }
        DbSet<BusRoutes> BusRoutes { get; set; }
        DbSet<Schedules> Schedules { get; set; }
        DbSet<Tickets> Tickets { get; set; }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

        }
    }
}
