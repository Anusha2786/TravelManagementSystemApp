namespace TravelManagementSystemApp.Models.Entities
{
    public class Trainsschedules
    {
        public int Schedule_ID { get; set; }
        public string Train_Number { get; set; }
        public string Departure_Station { get; set; }
        public string Arrival_Station { get; set; }
        public DateTime Departure_Time { get; set; }
        public DateTime Arrival_Time { get; set; }
        public int Total_Seats { get; set; }
        public int Available_Seats { get; set; }
        public string Frequency { get; set; }

        // Navigation properties to represent the relationship with Stations
        public Stations DepartureStationNavigation { get; set; }
        public Stations ArrivalStationNavigation { get; set; }
    }
}
