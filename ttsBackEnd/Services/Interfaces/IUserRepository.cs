using System;
using System.Threading.Tasks;
using ttsBackEnd.Models;

namespace ttsBackEnd.Services
{
    public interface IUserRepository
    {
        Task<Boolean> DeleteUser(int userId);
        Task<User> GetUser(int userId);
        Task<Boolean> UpdateLastOnline(int userId);
        Task<User[]> GetUsers();
        Task UpdateUser(User user);
    }
}