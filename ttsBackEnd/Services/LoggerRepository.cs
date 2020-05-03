using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ttsBackEnd.Data;
using ttsBackEnd.Models;

namespace ttsBackEnd.Services
{
    public class LoggerRepository : ILoggerRepository
    {
        private readonly DataContext _context;

        public LoggerRepository(DataContext context)
        {
            this._context = context;
        }

        public async Task LogActivity(LogActivity activity)
        {
            await _context.Activities.AddAsync(activity);
        }

        public async Task<LogActivity[]> GetActivities()
        {
            var activities = await _context.Activities.ToListAsync();
            return activities.ToArray();
        }

        public async Task<LogActivity[]> GetActivityForUser(int userId)
        {
            var activities = await _context.Activities.Where(x => x.UserID == userId).ToListAsync();
            return activities.ToArray();
        }

        public async Task<Boolean> DeleteActivity(int id)
        {
            var activity = await _context.Activities.FirstOrDefaultAsync(x => x.Id == id);
            if (activity == null) return false;
            _context.Activities.Remove(activity);
            return true;
        }

        public async Task LogSong(LogSong song)
        {
            await _context.LoggedSongs.AddAsync(song);
        }

        public async Task<LogSong[]> GetLoggedSongsForUser(int userId)
        {
            var songs = await _context.LoggedSongs.Where(x => x.UserId == userId).ToListAsync();
            if (songs.Count == 0) return null;
            return songs.ToArray();
        }

        public async Task DeleteSong(int logSongId)
        {
            var song = await _context.Activities.FirstOrDefaultAsync(x => x.Id == logSongId);
            _context.Remove(song);
        }

        public async Task<Boolean> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}