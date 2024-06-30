using Microsoft.EntityFrameworkCore;
using TravelManagementSystemApp.Models;
using TravelManagementSystemApp.Models.Entities;

namespace TravelManagementSystemApp.Data
{
    public class Hasslefreetraveldbcontext: DbContext
    {
        public Hasslefreetraveldbcontext(DbContextOptions<Hasslefreetraveldbcontext> options) : base(options)
        {
        }
            public DbSet<Flights> Flights { get; set; }
            public DbSet<Trains> Trains { get; set; }
        public DbSet<Buses> Buses { get; set; }
        public DbSet<Cabs> Cabs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
      
            modelBuilder.Entity<Flights>()
           .HasKey(f => f.Flight_ID);

            modelBuilder.Entity<Trains>()
           .HasKey(f => f.Train_ID);

            modelBuilder.Entity<Stations>()
                .HasKey(s => s.Station_Code); // Define StationCode as primary key
        }
    }
}
