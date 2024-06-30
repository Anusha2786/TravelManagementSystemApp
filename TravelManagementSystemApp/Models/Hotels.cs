using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
namespace TravelManagementSystemApp.Models
{
    public class Hotels
    {
        [Key]
        public int HotelID { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Description { get; set; }

        [ForeignKey("Address")]
        public int AddressID { get; set; }
        public Addreses? Address { get; set; }  // Navigation property for related Address

        [Required]
        public int StarRating { get; set; }

        public ICollection<Rooms>? Rooms { get; set; }  // Navigation property for related Rooms
    }
}
