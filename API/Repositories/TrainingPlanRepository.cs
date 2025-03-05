using API.Data;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class TrainingPlanRepository : ITrainingPlanRepository
    {
        private readonly DataContext _context;
        public TrainingPlanRepository(DataContext context)
        {
            this._context = context;
        }

        public TrainingPlan CreateTrainingPlan(TrainingPlan newTrainingPlan)
        {
            _context.TrainingPlans.Add(newTrainingPlan);
            _context.SaveChanges();
            return newTrainingPlan;
        }

        public bool DeleteTrainingPlan(int trainingPlanId)
        {
            var trainingPlan = GetTrainingPlanById(trainingPlanId);
            if (trainingPlan == null) return false;

            _context.TrainingPlans.Remove(trainingPlan);
            return Save();
        }

        public TrainingPlan GetTrainingPlanById(int trainingPlanId)
        {
            return _context.TrainingPlans.Find(trainingPlanId);
        }

        public ICollection<TrainingPlan> GetTrainingPlans(int userId)
        {
            return _context.TrainingPlans
                .Where(tp => tp.UserId == userId)
                .ToList();
        }

        public ICollection<Training> GetAllPlanTrainings(int trainingPlanId)
        {
            return _context.Trainings
                .Where(t => t.TrainingPlanId == trainingPlanId)
                .ToList();
        }

        public bool UpdateTrainingPlan(TrainingPlan updatedTrainingPlan)
        {
            _context.TrainingPlans.Update(updatedTrainingPlan);
            return Save();
        }

        public bool TrainingPlanExists(int trainingPlanId)
        {
            return _context.TrainingPlans.Any(tp => tp.Id == trainingPlanId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0;
        }

        
    }
}
