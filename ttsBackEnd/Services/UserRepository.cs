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
            return await _context.Users.FirstOrDefaultAsync(x => x.UserID == userId);
        }

        public async Task<User[]> GetUsers()
        {
            return await _context.Users.ToArrayAsync();
        }

        public async Task<Song[]> GetUserFavoriteSongs(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserID == userId);
            return user.FavoriteSongs.ToArray();
        }

        public async Task AddSongToFavoriteToUser(int userId, Song song)
        {
            FavoriteSong favSong = new FavoriteSong();
            favSong.Name = song.Name;
            favSong.Album = song.Album;
            favSong.Artist = song.Artist;
            favSong.Cover_art_url = song.Cover_art_url;
            favSong.UserID =


        }

    }
}