using System.Threading.Tasks;
using ttsBackEnd.Models;

namespace ttsBackEnd.Services
{
    public interface IUserRepository
    {

        Task<User> GetUser(int userId);
        Task<User[]> GetUsers();
    }
}