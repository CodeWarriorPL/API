using System.ComponentModel.DataAnnotations;
namespace API.Models
{
    public class Exercise
    {
        [Key]
        public int Id { get; set; }
    
        public string ExerciseName { get; set; }
        public BodyPart BodyPart { get; set; } 

        public string imageUrl { get; set; } 
                                               
    }

}
