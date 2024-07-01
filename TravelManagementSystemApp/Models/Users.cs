using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.ComponentModel.DataAnnotations;

namespace TravelManagementSystemApp.Models
{
    public class Users
    {
        [Key]
        public int userid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public int Phonenumber { get; set; }
    }
}
