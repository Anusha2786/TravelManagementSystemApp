namespace TravelManagementSystemApp.Models
{
    public class Flightschedules
    {
        public int scheduleid { get; set; }
        public int FlightId { get; set; }
        public DateTime Departuretime { get; set; }
        public DateTime Arrivaltime { get; set; }
        public Flight Flight { get; set; }
    }
}
