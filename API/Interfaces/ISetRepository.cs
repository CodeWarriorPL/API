using API.Models;

namespace API.Interfaces
{
    public interface ISetRepository
    {
        ICollection<Set> GetSets(int trainingId);

        ICollection<Set> GetUserSetsByExerciseId(int userId, int exerciseId);

        ICollection<Set> GetSetsByExerciseId(int exerciseId, int trainingId);
        Set GetSetById(int setId);

        bool CreateSet(Set newSet);

        bool UpdateSet(Set updatedSet);
        bool DeleteSet(Set deletedSet);
        bool SetExists(int setId);

        bool DeleteSetsByExerciseId(int exerciseId, int trainingId);
        bool Save();
    }
}
