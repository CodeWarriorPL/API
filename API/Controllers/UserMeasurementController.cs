using API.Dto;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserMeasurementController : Controller
    {
        private readonly IUserMeasurementRepository _userMeasurementRepository;
        private readonly IMapper _mapper;

        public UserMeasurementController(IUserMeasurementRepository userMeasurementRepository, IMapper mapper)
        {
            _userMeasurementRepository = userMeasurementRepository;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<UserMeasurementDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetUserMeasurements(int userId)
        {
            var userMeasurements = _userMeasurementRepository.GetUserMeasurements(userId);
            if (userMeasurements == null)
            {
                return NotFound();
            }

            var userMeasurementsDto = _mapper.Map<List<UserMeasurementDto>>(userMeasurements);
            return Ok(userMeasurementsDto);
        }

        [HttpGet("latest/{userId}")]
        [ProducesResponseType(200, Type = typeof(UserMeasurementDto))]
        [ProducesResponseType(404)]
        public IActionResult GetLatestUserMeasurement(int userId)
        {
            var latestMeasurement = _userMeasurementRepository.GetLatestUserMeasurement(userId);
            if (latestMeasurement == null)
            {
                return NotFound();
            }

            var latestMeasurementDto = _mapper.Map<UserMeasurementDto>(latestMeasurement);
            return Ok(latestMeasurementDto);
        }

        [HttpPost("{userId}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateUserMeasurement(int userId, [FromBody] UserMeasurementDto newUserMeasurement)
        {
            if (newUserMeasurement == null)
            {
                return BadRequest("Invalid measurement data.");
            }
            Console.WriteLine($"Otrzymana data: {newUserMeasurement.MeasurementDate}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userMeasurement = _mapper.Map<UserMeasurement>(newUserMeasurement);
            userMeasurement.UserId = userId;

            var createdUserMeasurement = _userMeasurementRepository.CreateUserMeasurement(userMeasurement);
            if (createdUserMeasurement == null)
            {
                return BadRequest("Failed to create user measurement.");
            }

            return Ok(createdUserMeasurement);
        }

        [HttpDelete("{measurementId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUserMeasurement(int measurementId)
        {
            var measurement = _userMeasurementRepository.GetUserMeasurementById(measurementId);

            if (measurement == null)
            {
                return NotFound("Pomiar nie istnieje.");
            }

            var deleted = _userMeasurementRepository.DeleteUserMeasurement(measurement);

            if (!deleted)
            {
                return BadRequest("Nie udało się usunąć pomiaru.");
            }

            return NoContent(); // 204 - sukces, brak treści
        }

        


        // 🔹 Aktualizacja całej listy pomiarów
        [HttpPut("{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult UpdateUserMeasurements(int userId, [FromBody] List<UserMeasurementDto> updatedMeasurements)
        {
            if (updatedMeasurements == null || updatedMeasurements.Count == 0)
            {
                return BadRequest("No measurements provided.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userMeasurements = _mapper.Map<List<UserMeasurement>>(updatedMeasurements);

            if (!_userMeasurementRepository.UpdateUserMeasurements(userId, userMeasurements))
            {
                return BadRequest("Failed to update user measurements.");
            }

            return Ok("User measurements updated successfully.");
        }
    }
}
