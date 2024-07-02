namespace TravelManagementSystemApp
{
   
        public class PaymentDTO
        {
            public int Payment_ID { get; set; }
        public int Booking_ID { get; set; }
            public decimal Amount { get; set; }
            public DateTime Payment_Date { get; set; }
            public string Payment_Method { get; set; }
        }

    }

