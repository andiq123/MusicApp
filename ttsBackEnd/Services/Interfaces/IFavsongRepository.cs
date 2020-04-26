using System.Threading.Tasks;
using ttsBackEnd.Models;

namespace ttsBackEnd.Services
{
    public interface IFavsongRepository
    {
        Task<bool> RemoveFavoriteSongFromUser(int songId, int userId);
        Task<bool> AddSongToFavoriteToUser(int userId, Song song);
        Task<FavoriteSong[]> GetUserSong(int userId);
    }
}