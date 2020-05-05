using System;
using System.Threading.Tasks;
using ttsBackEnd.Models;

namespace ttsBackEnd.Services
{
    public interface IUserRepository
    {
        Task<bool> AddSongToFavoriteToUser(int userId, Song song);
        Task<bool> DeleteUser(int userId);
        Task<User> GetUser(int userId);
        Task<User[]> GetUsers();
        Task<bool> RemoveFavoriteSongFromUser(int userId, int songId);
        Task<FavoriteSong[]> GetUserFavSong(int userId);
        Task<bool> SaveAll();
        Task<bool> UpdateLastOnline(int userId);
        Task UpdateUser(User user);
    }
}