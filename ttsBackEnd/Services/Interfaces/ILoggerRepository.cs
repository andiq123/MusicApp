using System;
using System.Threading.Tasks;
using ttsBackEnd.Models;

namespace ttsBackEnd.Services
{
    public interface ILoggerRepository
    {

        Task<bool> DeleteActivity(int id);
        Task DeleteSong(int logSongId);
        Task<LogActivity[]> GetActivities();
        Task<LogActivity[]> GetActivityForUser(int userId);
        Task<LogSong[]> GetLoggedSongsForUser(int userId);
        Task LogActivity(LogActivity activity);
        Task LogSong(LogSong song);
        Task<bool> SaveAll();
    }
}