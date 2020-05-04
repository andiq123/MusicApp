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
            return true;
        }

        public async Task UpdateUser(User user)
        {
            var userFromDb = await _context.Users.FirstOrDefaultAsync(x => x.ID == user.ID);
            userFromDb.Username = user.Username;
            userFromDb.ProfilePicUrl = user.ProfilePicUrl;
            userFromDb.PasswordHash = user.PasswordHash;
            userFromDb.PasswordSalt = user.PasswordSalt;
        }

        public async Task<Boolean> DeleteUser(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.ID == userId);
            if (user == null) return false;
            _context.Users.Remove(user);
            return true;
        }

        public async Task<bool> AddSongToFavoriteToUser(int userId, Song song)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.ID == userId);
            if (user == null) return false;
            FavoriteSong favSong = new FavoriteSong();
            favSong.Name = song.Name;
            favSong.Album = song.Album;
            favSong.Artist = song.Artist;
            favSong.Cover_art_url = song.Cover_art_url;
            favSong.UserID = userId;
            favSong.User = user;
            await _context.FavSongs.AddAsync(favSong);
            return true;
        }

        public async Task<bool> RemoveFavoriteSongFromUser(int userId, int songId)
        {
            var favsong = await _context.FavSongs.FirstOrDefaultAsync(x => x.Id == songId);
            if (favsong == null) return false;
            if (favsong.UserID != userId) return false;
            _context.FavSongs.Remove(favsong);
            return true;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}