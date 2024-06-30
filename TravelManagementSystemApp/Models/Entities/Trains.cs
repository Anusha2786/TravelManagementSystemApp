using System.ComponentModel.DataAnnotations;

namespace TravelManagementSystemApp.Models.Entities
{
    public class Trains
    {
        [Key]
        public int Train_ID { get; set; }

        [Required]
        [StringLength(20)]
        public int Train_Number { get; set; }

        [Required]
        [StringLength(100)]
        public string Train_Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Departure_Station { get; set; }

        [Required]
        [StringLength(100)]
        public string Arrival_Station { get; set; }

        [Required]
        public int Total_Seats { get; set; }

        [Required]
        public int Available_Seats { get; set; }

    }
}
