using API.Data;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class UserMeasurementRepository : IUserMeasurementRepository
    {
        private readonly DataContext _context;

        public UserMeasurementRepository(DataContext context)
        {
            _context = context;
        }
        public UserMeasurement CreateUserMeasurement(UserMeasurement newUserMeasurement)
        {
            _context.UserMeasurements.Add(newUserMeasurement);
            _context.SaveChanges();
            return newUserMeasurement;
        }

        public UserMeasurement GetLatestUserMeasurement(int userId)
        {
            return _context.UserMeasurements.Where(um => um.UserId == userId).OrderByDescending(um => um.MeasurementDate).FirstOrDefault();
        }
        public ICollection<UserMeasurement> GetUserMeasurements(int UserId)
        {
            return _context.UserMeasurements.Where(um => um.UserId == UserId).ToList();
        }

        public UserMeasurement GetUserMeasurementById(int userMeasurementId)
        {
            return _context.UserMeasurements.FirstOrDefault(um => um.Id == userMeasurementId);
        }


        public bool UpdateUserMeasurement(UserMeasurement updatedUserMeasurement)
        {
            _context.UserMeasurements.Update(updatedUserMeasurement);
            return Save();
        }

        public bool UserMeasurementExists(int userMeasurementId)
        {
            return _context.UserMeasurements.Any(um => um.Id == userMeasurementId);
        }

        public bool UpdateUserMeasurements(int userId, List<UserMeasurement> measurements)
        {
            var existingMeasurements = _context.UserMeasurements.Where(m => m.UserId == userId).ToList();

            if (existingMeasurements == null)
                return false;

            // Usuń stare pomiary
            _context.UserMeasurements.RemoveRange(existingMeasurements);

            // Dodaj nowe pomiary
            _context.UserMeasurements.AddRange(measurements);

            return _context.SaveChanges() > 0;
        }

        public bool DeleteUserMeasurement(UserMeasurement measurement)
        {
            _context.UserMeasurements.Remove(measurement);
            return _context.SaveChanges() > 0;
        }




        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0 ? true : false;
        }

    }
}
