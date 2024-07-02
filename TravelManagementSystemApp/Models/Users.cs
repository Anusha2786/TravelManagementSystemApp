using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace TravelManagementSystemApp.Models
{
    public class Users
    {

         public int userid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Phonenumber { get; set; }
    }
}
