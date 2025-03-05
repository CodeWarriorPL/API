using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class Set
    {
        [Key]
        public int Id { get; set; }
        public int TrainingId { get; set; }
        public int Repetitions { get; set; }
        public float Weight { get; set; }

        [JsonIgnore]
        public Training Training { get; set; }
        public Exercise Exercise { get; set; }

        public int ExerciseId { get; set; }
    }
}
