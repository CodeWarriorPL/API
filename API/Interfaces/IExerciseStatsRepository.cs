using API.Dto;
namespace API.Interfaces
{
    public interface IExerciseStatsRepository
    {
        Task<List<ExerciseStatDto>> GetAverageOneRepMaxByWeightCategory(string exerciseName);
    }
}
