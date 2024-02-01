// Update IAppRepository interface

using API.Data;
using API.Interfaces;
using API.Models;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUserById(int id);
        User CreateUser(User newUser);
        User UpdateUser(User updatedUser);
        void DeleteUser(int id);

        
    }
}



