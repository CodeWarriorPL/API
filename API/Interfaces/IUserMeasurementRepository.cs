using API.Models;
namespace API.Interfaces
{
    public interface IUserMeasurementRepository
    {
        ICollection<UserMeasurement> GetUserMeasurements(int UserId);
        UserMeasurement GetUserMeasurementById(int userMeasurementId);

        UserMeasurement GetLatestUserMeasurement(int userId);
        UserMeasurement CreateUserMeasurement(UserMeasurement newUserMeasurement);

        bool UpdateUserMeasurement(UserMeasurement updatedUserMeasurement);
        bool DeleteUserMeasurement(UserMeasurement deletedUserMeasurement);
        bool UserMeasurementExists(int userMeasurementId);
        bool UpdateUserMeasurements(int userId, List<UserMeasurement> updatedMeasurements);

        bool Save();
    }
}
