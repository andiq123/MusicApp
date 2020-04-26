using System;
using System.Threading.Tasks;
using ttsBackEnd.Models;

namespace ttsBackEnd.Services
{
    public interface ILoggerRepository
    {

        Task<Boolean> DeleteActivity(int id);
        Task<LogActivity[]> GetActivities();
        Task<LogActivity[]> GetActivityForUser(int userId);
        Task LogActivity(LogActivity activity);
    }
}