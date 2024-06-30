using System.ComponentModel.DataAnnotations;

namespace TravelManagementSystemApp.Models.Entities
{
    public class Flightsschedules
    {
        [Key]
        public int Schedule_ID { get; set; }
        public int Flight_ID { get; set; }
        public DateTime Departure_Time { get; set; }
        public DateTime Arrival_Time { get; set; }
        public string Frequency { get; set; }

        // Navigation property to represent the relationship
        public Flights Flights { get; set; }
    }
}
