using API.Dto;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExerciseStatsController : Controller
    {
        private readonly ExerciseStatsService _exerciseStatsService;

        public ExerciseStatsController()
        {
            _exerciseStatsService = new ExerciseStatsService("Assets/weightlifting_with_body_weight.csv"); // Podmień na właściwą ścieżkę
        }

        [HttpGet("average-one-rep-max")]
        public async Task<ActionResult<List<ExerciseStatDto>>> GetAverageOneRepMax([FromQuery] string exerciseName)
        {
            var result = await _exerciseStatsService.GetAverageOneRepMaxByWeightCategory(exerciseName);
            return Ok(result);
        }
    }
}
