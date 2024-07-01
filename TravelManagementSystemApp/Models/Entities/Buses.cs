using System.ComponentModel.DataAnnotations;

namespace TravelManagementSystemApp.Models.Entities
{
    public class Buses
    {
        [Key]
        public int Bus_ID { get; set; }

        [Required]
        [StringLength(20)]
        public int Bus_Number { get; set; }

        [Required]
        [StringLength(100)]
        public string Bus_Name { get; set; }

        [Required]
        [StringLength(100)]
        public string From_Location{ get; set; }

        [Required]
        [StringLength(100)]
        public string To_Location{ get; set; }

        [Required]
        public int Total_Seats { get; set; }

        [Required]
        public int Available_Seats { get; set; }
    }
}
