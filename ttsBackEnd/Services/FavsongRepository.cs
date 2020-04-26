using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ttsBackEnd.Data;
using ttsBackEnd.Models;

namespace ttsBackEnd.Services
{
    public class FavsongRepository : IFavsongRepository
    {
        private readonly DataContext _context;

        public FavsongRepository(DataContext context)
        {
            this._context = context;
        }

        public async Task<FavoriteSong[]> GetUserSong(int userId)
        {
            var favSongs = await _context.FavSongs.Where(x => x.UserID == userId).ToListAsync();
            if (favSongs.Count == 0) return null;
            return favSongs.ToArray();
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
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFavoriteSongFromUser(int songId, int userId)
        {
            var favsong = await _context.FavSongs.FirstOrDefaultAsync(x => x.ID == songId);
            if (favsong == null) return false;
            if (favsong.UserID != userId) return false;
            _context.FavSongs.Remove(favsong);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}