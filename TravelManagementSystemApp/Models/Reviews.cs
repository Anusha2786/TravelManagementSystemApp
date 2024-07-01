using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TravelManagementSystemApp.Models
{
    public class Reviews
    {

        [Key]
        public int ReviewID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        

        [Required]
        [MaxLength(50)]
        public string? EntityType { get; set; }

        [Required]
        public int EntityID { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public string? Comment { get; set; }

        [Required]
        public DateTime ReviewDate { get; set; }
    }
}
