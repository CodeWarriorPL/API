using API.Data;
using API.Interfaces;
using API.Models;

namespace API.Repositories
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly DataContext _context;
        public TrainingRepository(DataContext context)
        {
            this._context = context;
        }


        public Training CreateTraining(Training newTraining)
        {
            _context.Trainings.Add(newTraining);
            _context.SaveChanges();
            return newTraining;
        }

        public bool DeleteTraining(int TrainingId)
        {
          _context.Trainings.Remove(GetTrainingById(TrainingId));
            return Save();
        }

        public Training GetTrainingById(int trainingId)
        {
            return _context.Trainings.ToList().Find(t => t.Id == trainingId);
        }
        public Training GetTrainingByName(string trainingName, int userId)
        {
            if (trainingName == "")
            {
                return null;
            }
            return _context.Trainings.ToList().Find(t => t.name == trainingName && t.UserId == userId);
        }

        public ICollection<Training> GetTrainings(int userId)
        {
            return _context.Trainings.Where(t => t.UserId == userId).OrderByDescending(t => t.TrainingDate).ToList();
        }

        public bool Save()
        {
           var saved = _context.SaveChanges();
           return saved >= 0 ? true : false;
        }

        public bool TrainingExists(int trainingId)
        {
            return _context.Trainings.Any(t => t.Id == trainingId);
        }

        public bool UpdateTraining(Training updatedTraining)
        {
            _context.Trainings.Update(updatedTraining);
            return Save();
        }
       
    }
}
