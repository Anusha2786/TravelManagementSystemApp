using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelManagementSystemApp.Models
{
    public class Addreses
    {
        [Key]
        public int AddressID { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Street { get; set; }

        [Required]
        [MaxLength(100)]
        public string? City { get; set; }

        [Required]
        [MaxLength(100)]
        public string? State { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Country { get; set; }

        [Required]
        [MaxLength(20)]
        public string? PostalCode { get; set; }
    
}
}
