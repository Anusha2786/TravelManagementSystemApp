namespace TravelManagementSystemApp
{
    public class PutFlightsDTO
    {
        public string Flight_Number { get; set; }
        public string Airline { get; set; }
        public string Departure_Airport { get; set; }
        public string Arrival_Airport { get; set; }
        public int Total_Seats { get; set; }
        public int Available_Seats { get; set; }
    }
}
