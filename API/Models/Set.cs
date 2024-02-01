using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Set
    {
        [Key]
        public int Id { get; set; }
        public int TrainingId { get; set; }
        public int Repetitions { get; set; }
        public float Weight { get; set; }

        public Training Training { get; set; }
        public Exercise Exercise { get; set; }

        public int ExerciseId { get; set; }
    }
}
