using TravelManagementSystemApp.Models.Entities;

namespace TravelManagementSystemApp.Models
{
    public class Busschedules
    {
        public int scheduleid { get; set; }
        public int BusId { get; set; }
        public DateTime Departuretime { get; set; }
        public DateTime Arrivaltime { get; set; }
        public Buses Buses { get; set; }
    }
}
