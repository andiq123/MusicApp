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
            await _context.AddAsync(activity);
            await _context.SaveChangesAsync();
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
            await _context.SaveChangesAsync();
            return true;
        }
    }
}