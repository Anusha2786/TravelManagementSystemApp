using System.ComponentModel.DataAnnotations;

namespace TravelManagementSystemApp.Models.Entities
{
    public class Flights
    {
        [Key]
        public int Flight_ID { get; set; }
        public string Flight_Number { get; set; }
        public string Airline { get; set; }
        public string Departure_Airport { get; set; }
        public string Arrival_Airport { get; set; }
        public int Total_Seats { get; set; }
        public int Available_Seats { get; set; }
        public Flightschedules Flightschedules { get; set; }


    }
}
