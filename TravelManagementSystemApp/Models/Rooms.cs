using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TravelManagementSystemApp.Models
{
    public class Rooms
    {
        [Key]
        public int RoomID { get; set; }

        [ForeignKey("Hotel")]
        public int HotelID { get; set; }
        public Hotels? Hotel { get; set; }  // Navigation property for related Hotel

        [Required]
        [MaxLength(50)]
        public string? RoomType { get; set; }  // e.g., Single, Double, Suite

        [Required]
        public decimal PricePerNight { get; set; }

        [Required]
        public bool IsAvailable { get; set; }  // Indicates if the room is available for booking
    }
}
