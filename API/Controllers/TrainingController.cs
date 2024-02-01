using API.Dto;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainingController : Controller
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IMapper _mapper;



        public TrainingController(ITrainingRepository trainingRepository, IMapper mapper)
        {
            _trainingRepository = trainingRepository;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Training>))]
        [ProducesResponseType(404)]
        public IActionResult GetTrainings(int userId)
        {
            var trainings = _mapper.Map<IEnumerable<TrainingDto>>(_trainingRepository.GetTrainings(userId));


            if (!ModelState.IsValid)
            {
                return NotFound();
            }



            return Ok(trainings);
        }
        [HttpGet("byName/{trainingName}/{userId}")]
        [ProducesResponseType(200, Type = typeof(Training))]
        [ProducesResponseType(404)]
        public IActionResult GetTrainingByName(string trainingName, int userId)
        {
            var training = _mapper.Map<TrainingDto>(_trainingRepository.GetTrainingByName(trainingName, userId));
            if (!ModelState.IsValid)
            {
                return NotFound();

            }
            return Ok(training);
        }

        [HttpPost("{Id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateTraining(int Id, [FromBody] TrainingDto newTraining)
        {
            if (newTraining == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var trainingMap = _mapper.Map<Training>(newTraining);
            trainingMap.UserId = Id;

            var createdTraining = _trainingRepository.CreateTraining(trainingMap);

            return Ok(createdTraining);
        }

        [HttpGet("byId/{trainingId}")]
        public IActionResult GetTrainingById(int trainingId)
        {
            var training = _mapper.Map<Training>(_trainingRepository.GetTrainingById(trainingId));

            if (!_trainingRepository.TrainingExists(trainingId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(training);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTraining([FromBody] TrainingDto updatedTrainingDto)
        {
            if (updatedTrainingDto == null)
            {
                return BadRequest();
            }


            var trainingEntity = _mapper.Map<Training>(updatedTrainingDto);
            var updatedTraining = _trainingRepository.UpdateTraining(trainingEntity);

            return Ok(updatedTraining);
        }
        [HttpDelete("{trainingId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult DeleteTraining(int trainingId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
           
            _trainingRepository.DeleteTraining(trainingId);
            return Ok();
            

        }
    }
}
