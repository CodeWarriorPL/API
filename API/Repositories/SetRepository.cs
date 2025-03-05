using API.Data;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class SetRepository : ISetRepository
    {
        private readonly DataContext _context;

        public SetRepository(DataContext context)
        {
               _context = context;
        }
        public bool CreateSet(Set newSet)
        {
            _context.Sets.Add(newSet);
            return Save();
        }

        public bool DeleteSet(Set deletedSet)
        {
            
            _context.Sets.Remove(deletedSet);
            return Save();
        }

        public Set GetSetById(int setId)
        {
            return _context.Sets.ToList().Find(s => s.Id == setId);
        }

        public ICollection<Set> GetSets(int trainingId)
        {
            return _context.Sets.Where(s => s.TrainingId == trainingId).ToList();
        }
        public ICollection<Set> GetUserSetsByExerciseId(int userId, int exerciseId)
        {
            return _context.Sets.Include(s => s.Training).Where(s => s.Training.UserId == userId && s.ExerciseId == exerciseId).ToList();
        }   

        public ICollection<Set> GetSetsByExerciseId(int exerciseId, int trainingId)
        {
            return _context.Sets.Where(s => s.ExerciseId == exerciseId && s.TrainingId == trainingId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public bool SetExists(int setId)
        {
            return _context.Sets.Any(s => s.Id == setId);
        }   

        public bool UpdateSet(Set updatedSet)
        {
           var result  = GetSetById(updatedSet.Id);
            if (result != null)
            {
                result.Weight = updatedSet.Weight;
                result.Repetitions = updatedSet.Repetitions;
                result.Exercise = updatedSet.Exercise;
                result.ExerciseId = updatedSet.ExerciseId;
                result.Training = updatedSet.Training;
                result.TrainingId = updatedSet.TrainingId;
                    
            }
            _context.Sets.Update(result);


            return Save();
        }
        public bool DeleteSetsByExerciseId(int exerciseId, int trainingId)
        {
            var sets = _context.Sets.Where(s => s.ExerciseId == exerciseId && s.TrainingId == trainingId);
            _context.Sets.RemoveRange(sets);
            return Save();
        }
    }
}
