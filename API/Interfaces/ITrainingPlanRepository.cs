using API.Models;

namespace API.Interfaces
{
    public interface ITrainingPlanRepository
    {
        TrainingPlan CreateTrainingPlan(TrainingPlan newTrainingPlan);
        bool DeleteTrainingPlan(int trainingPlanId);
        TrainingPlan GetTrainingPlanById(int trainingPlanId);
        ICollection<TrainingPlan> GetTrainingPlans(int userId);
        ICollection<Training> GetAllPlanTrainings(int trainingPlanId);
        bool UpdateTrainingPlan(TrainingPlan updatedTrainingPlan);
        bool TrainingPlanExists(int trainingPlanId);
        bool Save();
    }
}
