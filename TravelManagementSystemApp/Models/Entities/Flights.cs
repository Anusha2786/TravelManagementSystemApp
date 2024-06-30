namespace TravelManagementSystemApp.Models.Entities
{
    public class Flights
    {
        [Key]
        public int FlightID { get; set; }
        public string FlightNumber { get; set; }
        public string Airline { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        // Navigation property to represent the relationship with FlightsSchedules
        public ICollection<Flightsschedules> Flightsschedules { get; set; }
    }
}
