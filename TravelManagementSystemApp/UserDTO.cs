using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TravelManagementSystemApp
{
    public class UserDTO
    {
        public int userid { get; set; } // Assuming userid is of type int
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public long Phonenumber { get; set; }
    }
}

