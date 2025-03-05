using API.Dto;
using API.Interfaces;
using API.Models;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainingController : Controller
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly ISetRepository _setRepository;
        private readonly IMapper _mapper;

        public TrainingController(ITrainingRepository trainingRepository, ISetRepository setRepository, IMapper mapper)
        {
            _trainingRepository = trainingRepository;
            _setRepository = setRepository; // Naprawione przypisanie!
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

        [HttpGet("plans/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Training>))]
        [ProducesResponseType(404)]
        public IActionResult GetTrainingPlans(int userId)
        {
            var trainingPlans = _mapper.Map<IEnumerable<TrainingDto>>(_trainingRepository.GetTrainingPlans(userId));

            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            return Ok(trainingPlans);
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

        [HttpGet("exercises/{trainingId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<int>))]
        [ProducesResponseType(404)]
        public IActionResult GetTrainingExercisesIds(int trainingId)
        {
            var exercisesIds = _trainingRepository.GetTrainingExercisesIds(trainingId);

            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            return Ok(exercisesIds);
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
        [HttpGet("sortedSets/{trainingId}")]
        [ProducesResponseType(200, Type = typeof(Dictionary<string, List<Set>>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetTrainingWithSetsGroupedByExercise(int trainingId)
        {
            var training = await _trainingRepository.GetTrainingWithSets(trainingId);

            if (training == null)
            {
                return NotFound("Training not found.");
            }

            // Grupowanie setów według ćwiczeń
            var groupedSets = training.Sets
                .GroupBy(s => s.Exercise.ExerciseName) // Grupujemy po nazwie ćwiczenia
                .ToDictionary(g => g.Key, g => g.ToList()); // Tworzymy słownik {nazwa ćwiczenia: lista setów}

            return Ok(groupedSets);
        }

        [HttpPut("updateTrainingName/{trainingId}/{newName}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult UpdateTrainingName(int trainingId, string newName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var updated = _trainingRepository.UpdateTrainigName(trainingId, newName);

            if (!updated)
            {
                return BadRequest();
            }

            return Ok();
        }
        [HttpPost("createWithSets/{userId}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateTrainingWithSets(int userId, [FromBody] TrainingWithSetsDto trainingWithSetsDto)
        {
            if (trainingWithSetsDto == null || trainingWithSetsDto.Sets == null)
            {
                return BadRequest("Nieprawidłowe dane wejściowe.");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var training = _mapper.Map<Training>(trainingWithSetsDto);
            training.UserId = userId;

            // Upewniamy się, że sety są poprawnie przypisane do treningu
            foreach (var set in training.Sets)
            {
                set.Training = training; // Zapewnia poprawne powiązanie w bazie
            }

            // Zapisujemy trening (i automatycznie sety, jeśli jest CascadeType.ALL)
            var createdTraining = _trainingRepository.CreateTraining(training);

            return Ok(createdTraining);
        }



        [HttpDelete("deleteExercise/{trainingId}/{exerciseId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult DeleteExerciseFromTraining(int trainingId, int exerciseId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var deleted = _trainingRepository.DeleteExerciseFromTraining(trainingId, exerciseId);

            if (!deleted)
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}
