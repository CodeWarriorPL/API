using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class UserMeasurement
    {
        [Key]
        public int Id { get; set; }
        public float Weight { get; set; }
        public DateTime MeasurementDate { get; set; }
        public int UserId { get; set; } // Klucz obcy
        [ForeignKey("UserId")]
        public User User { get; set; }
    }

}

