using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Training
    {
        [Key]
        public int Id { get; set; }
       
        public DateTime TrainingDate { get; set; }

        public string name { get; set; } 
        public int UserId { get; set; }

        public User User { get; set; } = null!;

        public ICollection<Set> Sets { get; set; } = new List<Set>();
        
    }
}
