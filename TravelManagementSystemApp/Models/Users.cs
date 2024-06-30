using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace TravelManagementSystemApp.Models
{
    public class Users
    {
        public int userId;
        public string Firstname;
        public string Lastname;
        public string Email;
        public string Password;
        public int Phonenumber;
    }
}
