using Microsoft.EntityFrameworkCore;
using TravelManagementSystemApp.Models.Entities;

namespace TravelManagementSystemApp.Data
{
    public class Hasslefreetraveldbcontext: DbContext
    {
        public Hasslefreetraveldbcontext(DbContextOptions<Hasslefreetraveldbcontext> options) : base(options)
        {
        }
            public DbSet<Flights> Flights { get; set; }
            public DbSet<Flightsschedules> FlightSchedules { get; set; }
            public DbSet<Trainsschedules> Trainschedules { get; set; }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationship between Flights and FlightSchedules
            modelBuilder.Entity<Flights>()
                .HasMany(f => f.Flightsschedules) // A Flight can have many FlightSchedules
                .WithOne(fs => fs.Flights) // Each FlightSchedule belongs to one Flight
                .HasForeignKey(fs => fs.Flight_ID); // Define the foreign key

            modelBuilder.Entity<Flights>()
           .HasKey(f => f.Flight_ID);

            modelBuilder.Entity<Flightsschedules>()
                .HasKey(fs => fs.Schedule_ID);

            // Optionally configure relationships here using modelBuilder.Entity<T>()
            modelBuilder.Entity<Flightsschedules>()
                .HasOne(fs => fs.Flights)
                .WithMany(f => f.Flightsschedules)
                .HasForeignKey(fs => fs.Flight_ID);

            modelBuilder.Entity<Trainsschedules>()
                .HasKey(ts => ts.Schedule_ID); // Define ScheduleID as primary key

            modelBuilder.Entity<Trainsschedules>()
                .HasOne(ts => ts.DepartureStationNavigation)
                .WithMany()
                .HasForeignKey(ts => ts.Departure_Station)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); // Configure DepartureStation as foreign key to Stations

            modelBuilder.Entity<Trainsschedules>()
                .HasOne(ts => ts.ArrivalStationNavigation)
                .WithMany()
                .HasForeignKey(ts => ts.Arrival_Station)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); // Configure ArrivalStation as foreign key to Stations

            modelBuilder.Entity<Stations>()
                .HasKey(s => s.Station_Code); // Define StationCode as primary key
        }
    }
}
