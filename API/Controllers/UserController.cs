using API.Dto;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using API.Data;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public UserController(IUserRepository userRepository, IMapper mapper, DataContext _context)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            this._context = _context;


        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<User>))]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        public IActionResult GetUserById(int id)
        {
            var user = _mapper.Map<UserDto>(_userRepository.GetUserById(id));

            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return BadRequest(); // 400 Bad Request
            }

            var createdUser = _userRepository.CreateUser(newUser);

            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (updatedUser == null || id != updatedUser.Id)
            {
                return BadRequest(); // 400 Bad Request
            }

            var existingUser = _userRepository.GetUserById(id);

            if (existingUser == null)
            {
                return NotFound(); // 404 Not Found
            }

            var user = _userRepository.UpdateUser(updatedUser);

            return Ok(user);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int id)
        {
            var userToDelete = _userRepository.GetUserById(id);

            if (userToDelete == null)
            {
                return NotFound(); // 404 Not Found
            }

            _userRepository.DeleteUser(id);

            return NoContent(); // 204 No Content
        }
        [HttpGet("one-rep-max-history")]
        [ProducesResponseType(200, Type = typeof(List<object>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOneRepMaxHistory([FromQuery] int userId, [FromQuery] string exerciseName)
        {
            var oneRepMaxResults = await _context.Trainings
                .Where(t => t.UserId == userId && t.TrainingPlanId == null)  // Filtrujemy tylko te treningi, które nie mają przypisanego TrainingPlanId
                .SelectMany(t => t.Sets
                    .Where(s => s.Exercise.ExerciseName == exerciseName)
                    .GroupBy(s => s.TrainingId)
                    .Select(g => new
                    {
                        Date = g.First().Training.TrainingDate,
                        OneRepMax = g.Max(s => s.Weight * (1 + s.Repetitions / 30.0f)) // Epley formula
                    })
                )
                .OrderBy(x => x.Date)
                .ToListAsync();

            return Ok(oneRepMaxResults);
        }



        // Training methods






    }
}