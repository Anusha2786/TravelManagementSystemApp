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
        public DbSet<Bookings>Bookings { get; set; }
        public DbSet<Booking_Details> Booking_Details { get; set; }
        public DbSet<Users> users { get; set; }

        //public DbSet<Bus> buses { get; set; }
        public DbSet<Busschedules> busschedules { get; set; }
        
        public DbSet<Trainschedules> trainschedules { get; set; }
      
        public DbSet<Flightschedules> flightschedules { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<Addreses> Addreses { get; set; } = default!;
        public DbSet<Airports> Airports { get; set; } = default!;
         public DbSet<Stations> Stations { get; set; } = default!;
        public DbSet<Hotels> Hotels { get; set; } = default!;
         public DbSet<Rooms> Rooms { get; set; } = default!;
        public DbSet<Reviews> Reviews { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
      
            modelBuilder.Entity<Flights>()
           .HasKey(f => f.Flight_ID);

            modelBuilder.Entity<Trains>()
           .HasKey(f => f.Train_ID);

            modelBuilder.Entity<Users>()
                .HasKey(f => f.userid);

            // Configure entity mappings, relationships, etc.
            modelBuilder.Entity<Bookings>()
                .HasKey(b => b.Booking_ID);
            //-------------------------------------------------------------------------------------------------
            modelBuilder.Entity<Busschedules>()
               .HasKey(bs => bs.scheduleid);

            modelBuilder.Entity<Trainschedules>()
                .HasKey(ts => ts.scheduleid);

            modelBuilder.Entity<Flightschedules>()
                .HasKey(fs => fs.scheduleid);

            modelBuilder.Entity<Buses>()
                .HasOne(b => b.Busschedule)
                .WithOne(bs => bs.Buses)
                .HasForeignKey<Busschedules>(bs => bs.BusId);

            modelBuilder.Entity<Trains>()
                .HasOne(t => t.Trainschedules)
                .WithOne(ts => ts.Trains)
                .HasForeignKey<Trainschedules>(ts => ts.TrainId);

            modelBuilder.Entity<Flights>()
                 .HasOne(f => f.Flightschedules) // Flights has one Flightschedules
                 .WithOne(fs => fs.Flights)       // Flightschedules has one Flight
                 .HasForeignKey<Flightschedules>(fs => fs.FlightId); // Foreign key FlightId in Flightschedules
            //------------------------------------------------------------------------------------------------------------------

            /// Define relationships (if not using conventions)
            modelBuilder.Entity<Bookings>()
                .HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.userid);

            // Configure entity mappings, relationships, etc.
        modelBuilder.Entity<Booking_Details>()
            .HasKey(bd => bd.Booking_Detail_ID);

            // Define relationships (e.g., foreign keys)
            modelBuilder.Entity<Booking_Details>()
                .HasOne(bd => bd.Bookings)
                .WithMany()
                .HasForeignKey(bd => bd.Booking_ID);


       

            // Define relationships (e.g., foreign keys)
            modelBuilder.Entity<Payments>()
                .HasOne(p => p.Bookings)
                .WithMany()
                .HasForeignKey(p => p.Booking_ID);
            modelBuilder.Entity<Stations>()
                .HasKey(s => s.Code); // Define StationCode as primary key
            // Configure relationships and constraints here if needed
            modelBuilder.Entity<Booking_Details>()
                .HasOne(bd => bd.Bookings)
                .WithMany(b => b.Booking_Details)
                .HasForeignKey(bd => bd.Booking_ID)
                .OnDelete(DeleteBehavior.Cascade); // Example of cascade delete, adjust as per your requirements
            modelBuilder.Entity<Payments>()
                .HasKey(p => p.Payment_ID); // Define PaymentID as primary key

            modelBuilder.Entity<Payments>()
                .Property(p => p.Payment_ID)
                .ValueGeneratedOnAdd(); // Auto-generate PaymentID on insert

            modelBuilder.Entity<Payments>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(10, 2)") // Specify column type for Amount
                .IsRequired(); // Ensure Amount is required

            modelBuilder.Entity<Payments>()
                .Property(p => p.Payment_Method)
                .HasMaxLength(50) // Specify max length for PaymentMethod
                .IsRequired(); // Ensure PaymentMethod is required

            modelBuilder.Entity<Payments>()
                .HasOne(p => p.Bookings)
                .WithMany(b => b.Payments) // Define navigation property to Booking
                .HasForeignKey(p => p.Booking_ID) // Define foreign key relationship
                .OnDelete(DeleteBehavior.Restrict); // Define delete behavior (if needed)

        }
    }
}
