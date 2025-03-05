using API.Models;

namespace API.Interfaces
{
    public interface ITrainingRepository
    {
        ICollection<Training> GetTrainings(int userId);

        ICollection<Training> GetTrainingPlans(int userId);

        ICollection<int> GetTrainingExercisesIds(int trainingId);

        Training GetTrainingById(int trainingId);

        Training GetTrainingByName (string trainingName, int userId);

        Training CreateTraining(Training newTraining);

        Task<Training> GetTrainingWithSets(int trainingId);

        public Training CreateTrainingWithSets(Training newTraining, List<Set> sets);


        bool UpdateTraining(Training updatedTraining);

        bool UpdateTrainigName(int trainingId, string newName);

        bool DeleteTraining(int trainingId);

        bool DeleteExerciseFromTraining(int trainingId, int exerciseId);

        bool TrainingExists(int trainingId);    
        bool Save();


        
    }
}
