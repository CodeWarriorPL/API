using API.Data;
using API.Interfaces;
using API.Models;
using System.Collections.Generic;
using System.Linq;

namespace API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            this._context = context;
        }

        public ICollection<User> GetUsers()
        {

            return _context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        public User CreateUser(User newUser)
        {
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return newUser;
        }

        public User UpdateUser(User updatedUser)
        {
            _context.Users.Update(updatedUser);
            _context.SaveChanges();
            return updatedUser;
        }

        public void DeleteUser(int id)
        {
            var userToDelete = _context.Users.Find(id);
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
            }
        }

        public ICollection<Training> GetTrainings(int id)
        {
            return _context.Trainings.Where(t => t.UserId == id).ToList();
        }

        public Training GetTrainingById(int Trainingid, int UserId)
        {
            return _context.Trainings.Where(t => t.Id == Trainingid && t.UserId == UserId).FirstOrDefault();
        }

        public bool CreateTraining(Training newTraining)
        {
            _context.Add(newTraining);
            return Save();
        }

        public bool UpdateTraining(Training updatedTraining, int UserId)
        {
            var existingTraining = _context.Trainings.FirstOrDefault(t => t.Id == updatedTraining.Id && t.UserId == UserId);

            if (existingTraining != null)
            {
                existingTraining.TrainingDate = updatedTraining.TrainingDate;
                _context.SaveChanges();
                return true;
            }

            return false; 
        }

        public void DeleteTraining(int id)
        {
            var trainingToDelete = _context.Trainings.Find(id);
            if (trainingToDelete != null)
            {
                _context.Trainings.Remove(trainingToDelete);
                _context.SaveChanges();
            }
        }
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
    
}
