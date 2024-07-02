using System.Text.Json.Serialization;

namespace TravelManagementSystemApp.Models.Entities
{
    public class Booking_Details
    {
        
            public int Booking_Detail_ID { get; set; }
            public int Booking_ID { get; set; }
            public string Detail_Type { get; set; }
            public int Detail_ID { get; set; }

        // Navigation property to Booking entity
        // Navigation property
        [JsonIgnore] // Add this attribute to prevent serialization of Bookings
       
        public Bookings Bookings { get; set; }
    }

    
}
