using API.Data;
using API.Interfaces;
using API.Models;

namespace API.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly DataContext _context;
        public ExerciseRepository(DataContext context)
        {
            this._context = context;
        }
        public bool CreateExercise(Exercise newExercise)
        {
           _context.Exercises.Add(newExercise);
            return Save();
        }

        public bool DeleteExercise(Exercise deletedExercise)
        {
            _context.Exercises.Remove(deletedExercise);
            return Save();
        }

        public bool ExerciseExists(int exerciseId)
        {
            return (_context.Exercises.Find(exerciseId) != null);
        }

        public Exercise GetExerciseById(int exerciseId)
        {
            return(_context.Exercises.Find(exerciseId));
           
        }

        public ICollection<Exercise> GetExercises()
        {
            return _context.Exercises.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public bool UpdateExercise(Exercise updatedExercise)
        {
            _context.Exercises.Update(updatedExercise);
            return Save();
        }
    }
}
