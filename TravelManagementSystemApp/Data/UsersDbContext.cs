using Microsoft.EntityFrameworkCore;
using TravelManagementSystemApp.Models;

namespace TravelManagementSystemApp.Data
{
    public class UsersDbContext:DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }
        public DbSet<Users> users { get; set; }
        public DbSet<Busschedules> busschedules { get; set; }
        public DbSet<Trainschedules> trainschedules { get; set; }
        public DbSet <Flightschedules> Flightschedules { get; set; }
    }
}
