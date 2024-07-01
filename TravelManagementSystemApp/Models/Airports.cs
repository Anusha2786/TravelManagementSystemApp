
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
namespace TravelManagementSystemApp.Models
{
    public class Airports
    {

        [Key]
        public int AirportID { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string? Code { get; set; }

        [ForeignKey("Address")]
        public int AddressID { get; set; }
        public Addreses? Address { get; set; }  // Navigation property for related Address
    }
}
