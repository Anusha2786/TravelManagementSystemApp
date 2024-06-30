using System.ComponentModel.DataAnnotations;

namespace TravelManagementSystemApp.Models
{
    public class Train
    {
        [Key]
        public int Train_ID { get; set; }
        public string Train_Number { get; set; }
        public string Train_Operator { get; set; }
        public string Departure_Station { get; set; }
        public string Arrival_Station { get; set; }
        public int Total_Seats { get; set; }
        public int Available_Seats { get; set; }
        public Trainschedules Trainschedule { get; set; }
    }

}
