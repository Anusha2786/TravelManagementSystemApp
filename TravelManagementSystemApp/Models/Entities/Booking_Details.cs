namespace TravelManagementSystemApp.Models.Entities
{
    public class Booking_Details
    {
        
            public int Booking_Detail_ID { get; set; }
            public int Booking_ID { get; set; }
            public string Detail_Type { get; set; }
            public int Detail_ID { get; set; }

        // Navigation property to Booking entity
        public Bookings Bookings { get; set; }
    }

    
}
