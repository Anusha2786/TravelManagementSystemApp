using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TravelManagementSystemApp
{
    public class CabDTO
    {
        [Required]
        public string Cab_Model { get; set; }

        [Required]
        [StringLength(100)]
        public string Pickup_Location { get; set; }

        [Required]
        [StringLength(100)]
        public string Dropoff_Location { get; set; }

        [Required]
        public DateTime Pickup_Time { get; set; }

        public DateTime? Dropoff_Time { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal FareAmount { get; set; }
        public double Distance { get; set; }
        [StringLength(50)]
        public string CabType { get; set; }
    }
}

