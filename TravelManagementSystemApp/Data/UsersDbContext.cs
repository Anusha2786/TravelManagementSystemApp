using Microsoft.EntityFrameworkCore;
using TravelManagementSystemApp.Models;

namespace TravelManagementSystemApp.Data
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }
        public DbSet<Users> users { get; set; }
        public DbSet<Bus> buses { get; set; }
        public DbSet<Busschedules> busschedules { get; set; }
        public DbSet<Train> trains { get; set; }
        public DbSet<Trainschedules> trainschedules { get; set; }
        public DbSet<Flight> flights { get; set; }
        public DbSet<Flightschedules> flightschedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Busschedules>()
                .HasKey(bs => bs.scheduleid);

            modelBuilder.Entity<Trainschedules>()
                .HasKey(ts => ts.scheduleid);

            modelBuilder.Entity<Flightschedules>()
                .HasKey(fs => fs.scheduleid);

            modelBuilder.Entity<Bus>()
                .HasOne(b => b.Busschedule)
                .WithOne(bs => bs.Bus)
                .HasForeignKey<Busschedules>(bs => bs.BusId);

            modelBuilder.Entity<Train>()
                .HasOne(t => t.Trainschedule)
                .WithOne(ts => ts.Train)
                .HasForeignKey<Trainschedules>(ts => ts.TrainId);

            modelBuilder.Entity<Flight>()
                .HasOne(f => f.Flightschedule)
                .WithOne(fs => fs.Flight)
                .HasForeignKey<Flightschedules>(fs => fs.FlightId);
        }
    }
}