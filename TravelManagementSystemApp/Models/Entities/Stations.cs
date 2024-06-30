namespace TravelManagementSystemApp.Models.Entities
{
    public class Stations
    {
        public string StationCode { get; set; } // Primary key
        public string StationName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
