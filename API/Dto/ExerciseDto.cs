using API.Models;

namespace API.Dto
{
    public class ExerciseDto
    {
        public int Id { get; set; }
        public string ExerciseName { get; set; }

        public BodyPart BodyPart { get; set; }
        public string imageUrl { get; set; }
    }
}
