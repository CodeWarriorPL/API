using API.Data;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

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

        public bool UpdateTrainigName(int trainingId, string newName)
        {
            var training = GetTrainingById(trainingId);
            training.name = newName;
            _context.Trainings.Update(training);
            return Save();
        }   

        public ICollection<Training> GetTrainings(int userId)
        {
            return _context.Trainings.Where(t => t.UserId == userId && !t.IsTraingingPlan).OrderByDescending(t => t.TrainingDate).ToList();
        }

        public ICollection<Training> GetTrainingPlans(int userId)
        {
            return _context.Trainings.Where(t => t.UserId == userId && t.IsTraingingPlan == true).ToList();
        }

        public ICollection<int> GetTrainingExercisesIds(int trainingId)
        {
            return _context.Trainings.Find(trainingId).Sets.Select(s => s.ExerciseId).ToList();
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

        public bool DeleteExerciseFromTraining(int trainingId, int exerciseId)
        {
            var training = _context.Trainings
                                   .Include(t => t.Sets)
                                   .FirstOrDefault(t => t.Id == trainingId);

            if (training == null) return false;

            var setsToRemove = training.Sets.Where(s => s.ExerciseId == exerciseId).ToList();

            if (!setsToRemove.Any()) return false;

            _context.Sets.RemoveRange(setsToRemove);

            return Save();
        }
        public Training CreateTrainingWithSets(Training newTraining, List<Set> sets)
        {
            _context.Trainings.Add(newTraining);
            _context.SaveChanges(); // Najpierw zapisujemy trening, aby uzyskał ID

            foreach (var set in sets)
            {
                set.TrainingId = newTraining.Id; // Przypisujemy ID nowo utworzonego treningu
                _context.Sets.Add(set);
            }

            _context.SaveChanges();
            return newTraining;
        }

        public async Task<Training> GetTrainingWithSets(int trainingId)
        {
            return await _context.Trainings
                .Include(t => t.Sets)
                    .ThenInclude(s => s.Exercise) // Pobieramy ćwiczenia
                .FirstOrDefaultAsync(t => t.Id == trainingId);
        }



    }
}
