namespace TravelManagementSystemApp.Models
{
    public class Trainschedules
    {
        public int scheduleid { get; set; }
        public int TrainId { get; set; }
        public DateTime Departuretime { get; set; }
        public DateTime Arrivaltime { get; set; }
        public Train Train { get; set; }
    }
}
