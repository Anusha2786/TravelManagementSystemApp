using System.ComponentModel.DataAnnotations;

namespace TravelManagementSystemApp.Models
{
 
    public class Bus
    {
        [Key]
        public int Bus_ID { get; set; }
        public string Bus_Number { get; set; }
        public string Departure_Location { get; set; }
        public string Arrival_Location { get; set; }
        public int Total_Seats { get; set; }
        public int Available_Seats { get; set; }
        public Busschedules Busschedule { get; set; }
    }
}
