using API.Dto;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExerciseController : Controller
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;

        public ExerciseController(IExerciseRepository exerciseRepository, IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Exercise>))]
        public IActionResult GetExercises()
        {
            var exercises = _mapper.Map<ICollection<ExerciseDto>>(_exerciseRepository.GetExercises());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(exercises);

        }

        [HttpGet("{exerciseId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetExercise(int exerciseId)
        {
            var exercise = _mapper.Map<ExerciseDto>(_exerciseRepository.GetExerciseById(exerciseId));

            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            return Ok(exercise);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateExercise([FromBody] ExerciseDto newExercise)
        {
            if (newExercise == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exerciseMap = _mapper.Map<Exercise>(newExercise);

            var createdExercise = _exerciseRepository.CreateExercise(exerciseMap);

            return Ok(createdExercise);
        }
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateExercise([FromBody] ExerciseDto updatedExercise)
        {
            if (updatedExercise == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exerciseMap = _mapper.Map<Exercise>(updatedExercise);

            var updated = _exerciseRepository.UpdateExercise(exerciseMap);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{exerciseId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteExercise(int exerciseId)
        {
            if (!_exerciseRepository.ExerciseExists(exerciseId))
            {
                return NotFound();
            }

            var exerciseToDelete = _exerciseRepository.GetExerciseById(exerciseId);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var deleted = _exerciseRepository.DeleteExercise(exerciseToDelete);

            if (!deleted)
            {
                return BadRequest();
            }

            return NoContent();
        }
        [HttpGet("Name/{exerciseId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetExerciseName(int exerciseId)
        {
            var exercise = _exerciseRepository.GetExerciseById(exerciseId);

            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            return Ok(exercise.ExerciseName);
        }


    }
}
