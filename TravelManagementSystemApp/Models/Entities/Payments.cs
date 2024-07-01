namespace TravelManagementSystemApp.Models.Entities
{
    public class Payments
    {
        public int Payment_ID { get; set; }
        public int Booking_ID { get; set; }
        public decimal Amount { get; set; }
        public DateTime Payment_Date { get; set; }
        public string Payment_Method { get; set; }

        // Navigation property to Booking entity
        public Bookings Bookings { get; set; }
    }
}
