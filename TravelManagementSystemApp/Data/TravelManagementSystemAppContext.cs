using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelManagementSystemApp.Models;

namespace TravelManagementSystemApp.Data
{
    public class TravelManagementSystemAppContext : DbContext
    {
        public TravelManagementSystemAppContext (DbContextOptions<TravelManagementSystemAppContext> options)
            : base(options)
        {
        }

        public DbSet<TravelManagementSystemApp.Models.Addreses> Addreses { get; set; } = default!;
        public DbSet<TravelManagementSystemApp.Models.Airports> Airports { get; set; } = default!;
        public DbSet<TravelManagementSystemApp.Models.Stations> Stations { get; set; } = default!;
        public DbSet<TravelManagementSystemApp.Models.Hotels> Hotels { get; set; } = default!;
        public DbSet<TravelManagementSystemApp.Models.Rooms> Rooms { get; set; } = default!;
        public DbSet<TravelManagementSystemApp.Models.Reviews> Reviews { get; set; } = default!;
    }
}
