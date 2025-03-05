using API.Dto;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SetController : Controller
    {
        private readonly ISetRepository _setRepository;
        private readonly IMapper _mapper;

        public SetController(ISetRepository setRepository, IMapper mapper)
        {
            _setRepository = setRepository;
            _mapper = mapper;
        }

        [HttpGet("{trainingId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Set>))]
        public IActionResult GetSets(int trainingId)
        {
            var sets = _mapper.Map<ICollection<SetDto>>(_setRepository.GetSets(trainingId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(sets);

        }

        [HttpGet("byTrainingExercise/{exerciseId}/{trainingId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Set>))]
        public IActionResult GetSetsByExerciseId(int exerciseId, int trainingId)
        {
            var sets = _mapper.Map<ICollection<SetDto>>(_setRepository.GetSetsByExerciseId(exerciseId, trainingId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(sets);

        }

        [HttpGet("byId/{setId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]

        public IActionResult GetSet(int setId)
        {
            var set = _mapper.Map<SetDto>(_setRepository.GetSetById(setId));

            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            return Ok(set);
        }

        [HttpPost("{TrainingId}/{ExerciseId}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateSet([FromBody] SetDto newSet,int TrainingId,int ExerciseId)
        {
            if (newSet == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           
            var setMap = _mapper.Map<Set>(newSet);
            setMap.TrainingId = TrainingId;
            setMap.ExerciseId = ExerciseId;


            


            var createdSet = _setRepository.CreateSet(setMap);

            return Ok(createdSet);
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateSet([FromBody] SetDto updatedSet)
        {
            if (updatedSet == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var setMap = _mapper.Map<Set>(updatedSet);

           
            

            _setRepository.UpdateSet(setMap);

            return NoContent(); //success
        }
        [HttpDelete("{setId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteSet(int setId)
        {
            if (!_setRepository.SetExists(setId))
            {
                return NotFound();
            }

            var setToDelete = _setRepository.GetSetById(setId);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _setRepository.DeleteSet(setToDelete);

            return NoContent();
        }

        [HttpDelete("byExercise/{trainingId}/{exerciseId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteSetsByExercise (int trainingId, int exerciseId)
        {
          

           

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _setRepository.DeleteSetsByExerciseId(exerciseId,trainingId);

            return NoContent();
        }
        [HttpGet("byExercise/{userId}/{exerciseId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Set>))]
        public IActionResult GetUserSetsByExerciseId(int userId, int exerciseId)
        {
            var sets = _mapper.Map<ICollection<SetDto>>(_setRepository.GetUserSetsByExerciseId(userId, exerciseId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(sets);

        }




    }
}
