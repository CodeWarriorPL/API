using API.Models;

namespace API.Interfaces
{
    public interface IExerciseRepository
    {
        ICollection<Exercise> GetExercises();
        Exercise GetExerciseById(int exerciseId);
        bool CreateExercise(Exercise newExercise);
        bool UpdateExercise(Exercise updatedExercise);
        bool DeleteExercise(Exercise deletedExercise);
        bool ExerciseExists(int exerciseId);
        bool Save();

    }
}
