using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class User
    {
     [Key]
     public int Id { get; set; }
       
    public string Email { get; set; }
    public string Username{ get; set; }
    public string Password { get; set; }

    public ICollection<Training> Trainings { get;} = new List<Training>();

    public ICollection<UserMeasurement> UserMeasurements { get;} = new List<UserMeasurement>();

    public ICollection<TrainingPlan> TrainingPlans { get;} = new List<TrainingPlan>();
    
    }
}
