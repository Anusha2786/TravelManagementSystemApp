namespace TravelManagementSystemApp
{
    public class BookingsDTO
    {
        
        public DateTime Booking_Date { get; set; }
        public string? Booking_Type { get; set; }
        public string? Booking_Status { get; set; }
        public UserDTO? User { get; set; }
    }
}
