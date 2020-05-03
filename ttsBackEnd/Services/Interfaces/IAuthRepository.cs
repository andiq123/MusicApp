using System;
using System.Threading.Tasks;
using ttsBackEnd.Models;

namespace ttsBackEnd.Services
{
    public interface IAuthRepository
    {
        Task<User> Login(string username, string password);
        Task<User> Register(User user, string password);
        Task<bool> UserExists(string username);
        Task<Boolean> SaveAll();
    }
}