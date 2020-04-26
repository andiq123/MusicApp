using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ttsBackEnd.Data;
using ttsBackEnd.Models;

namespace ttsBackEnd.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            this._context = context;

        }

        public async Task<User> GetUser(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.ID == userId);
        }

        public async Task<User[]> GetUsers()
        {
            return await _context.Users.ToArrayAsync();
        }

        public async Task<Boolean> UpdateLastOnline(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.ID == userId);
            if (user == null) return false;
            user.LastOnline = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task UpdateUser(User user)
        {
            var userFromDb = await _context.Users.FirstOrDefaultAsync(x => x.ID == user.ID);
            userFromDb.UserName = user.UserName;
            userFromDb.Email = user.Email;
            userFromDb.ProfilePicUrl = user.ProfilePicUrl;
            userFromDb.PasswordHash = user.PasswordHash;
            userFromDb.PasswordSalt = user.PasswordSalt;
            await _context.SaveChangesAsync();
        }

        public async Task<Boolean> DeleteUser(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.ID == userId);
            if (user == null) return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}