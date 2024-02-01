using API.Models;

namespace API.Interfaces
{
    public interface ITrainingRepository
    {
        ICollection<Training> GetTrainings(int userId);
        Training GetTrainingById(int trainingId);

        Training GetTrainingByName (string trainingName, int userId);

        Training CreateTraining(Training newTraining);
       
        bool UpdateTraining(Training updatedTraining);

        bool DeleteTraining(int trainingId);

        bool TrainingExists(int trainingId);    
        bool Save();


        
    }
}
