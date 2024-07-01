namespace TravelManagementSystemApp.Models.Entities
{
    public class Bookings
    {
        public int Booking_ID { get; set; }
        public int userid { get; set; }
        public DateTime Booking_Date { get; set; }
        public string Booking_Type { get; set; }
        public string Booking_Status { get; set; }
        // Navigation property to User entity
        public Users User { get; set; }
        public ICollection<Booking_Details> Booking_Details { get; set; }
        // Navigation property to Payments entities
        public ICollection<Payments> Payments { get; set; }
    }
}
