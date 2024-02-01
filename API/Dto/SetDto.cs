namespace API.Dto
{
    public class SetDto
    {
        public int Id { get; set; }
        public int Repetitions { get; set; }
        public int Weight { get; set; }

        public int ExerciseId { get; set; }

        public int TrainingId { get; set; }
        
    }
}
