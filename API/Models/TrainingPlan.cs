using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class TrainingPlan
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int UserId { get; set; } 
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        public ICollection<Training> Trainings { get; set; } = new List<Training>();
    }
}
