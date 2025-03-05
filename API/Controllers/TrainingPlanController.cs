using Microsoft.AspNetCore.Mvc;
using API.Interfaces;
using AutoMapper;
using API.Models;
using API.Dto;
using System.Collections.Generic;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainingPlanController : ControllerBase
    {
        private readonly ITrainingPlanRepository _trainingPlanRepository;
        private readonly IMapper _mapper;

        public TrainingPlanController(ITrainingPlanRepository trainingPlanRepository, IMapper mapper)
        {
            _trainingPlanRepository = trainingPlanRepository;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<TrainingPlanDto>))]
        public IActionResult GetTrainingPlans(int userId)
        {
            var trainingPlans = _trainingPlanRepository.GetTrainingPlans(userId);
            if (trainingPlans == null) return NotFound();

            var mappedTrainingPlans = _mapper.Map<ICollection<TrainingPlanDto>>(trainingPlans);
            return Ok(mappedTrainingPlans);
        }

        [HttpGet("plan/{id}")]
        [ProducesResponseType(200, Type = typeof(TrainingPlanDto))]
        [ProducesResponseType(404)]
        public IActionResult GetTrainingPlan(int id)
        {
            var trainingPlan = _trainingPlanRepository.GetTrainingPlanById(id);
            if (trainingPlan == null) return NotFound();

            var mappedTrainingPlan = _mapper.Map<TrainingPlanDto>(trainingPlan);
            return Ok(mappedTrainingPlan);
        }

        [HttpGet("all/{trainingPlanId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<TrainingDto>))]
        public IActionResult GetAllPlanTrainings(int trainingPlanId)
        {
           var trainings = _trainingPlanRepository.GetAllPlanTrainings(trainingPlanId);
            if (trainings == null) return NotFound();

            var mappedTrainings = _mapper.Map<ICollection<TrainingDto>>(trainings);
            return Ok(mappedTrainings);
        }

        [HttpPost("{userId}")]
        [ProducesResponseType(201, Type = typeof(TrainingPlanDto))]
        [ProducesResponseType(400)]
        public IActionResult CreateTrainingPlan(int userId, [FromBody] TrainingPlanDto trainingPlanDto)
        {
            if (trainingPlanDto == null) return BadRequest();

            var trainingPlan = _mapper.Map<TrainingPlan>(trainingPlanDto);
            trainingPlan.UserId = userId;

            var createdPlan = _trainingPlanRepository.CreateTrainingPlan(trainingPlan);
            if (createdPlan == null) return BadRequest("Failed to create training plan");

            var mappedCreatedPlan = _mapper.Map<TrainingPlanDto>(createdPlan);
            return CreatedAtAction(nameof(GetTrainingPlan), new { id = mappedCreatedPlan.Id }, mappedCreatedPlan);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(TrainingPlanDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTrainingPlan(int id, [FromBody] TrainingPlanDto trainingPlanDto)
        {
            if (trainingPlanDto == null || id != trainingPlanDto.Id) return BadRequest();

            var trainingPlan = _mapper.Map<TrainingPlan>(trainingPlanDto);
            if (!_trainingPlanRepository.TrainingPlanExists(id)) return NotFound();

            var updatedPlan = _trainingPlanRepository.UpdateTrainingPlan(trainingPlan);
            if (!updatedPlan) return BadRequest("Failed to update training plan");

            return Ok(trainingPlanDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTrainingPlan(int id)
        {
            if (!_trainingPlanRepository.TrainingPlanExists(id)) return NotFound();

            if (!_trainingPlanRepository.DeleteTrainingPlan(id)) return BadRequest("Failed to delete training plan");

            return NoContent();
        }

    }
}
