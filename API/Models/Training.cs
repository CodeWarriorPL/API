using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Training
    {
        [Key]
        public int Id { get; set; }
       
        public DateTime TrainingDate { get; set; }

        public string name { get; set; } 
        public int UserId { get; set; }
        
        public Boolean IsTraingingPlan { get; set; }

        public int? TrainingPlanId { get; set; }
        [ForeignKey("TrainingPlanId")]
        public TrainingPlan? TrainingPlan { get; set; }  

        public User User { get; set; } = null!;

        public ICollection<Set> Sets { get; set; } = new List<Set>();
        
    }
}
