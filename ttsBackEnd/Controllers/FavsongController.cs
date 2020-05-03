using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ttsBackEnd.Models;
using ttsBackEnd.Services;

namespace ttsBackEnd.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users/id/[controller]")]
    public class FavsongController : ControllerBase
    {
        private readonly int _userId;
        private readonly IUserRepository _repo;

        public FavsongController(IUserRepository repo)
        {
            this._repo = repo;
            this._userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserFavSongs(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var user = await _repo.GetUser(userId);
            if (user == null) return NotFound("No user found");
            var songs = user.FavSongs;
            if (songs.Count == 0) return NotFound("Empty favorite song library");
            return Ok(songs);
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> AddSongToFavorite(int userId, Song song)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var successAdded = await _repo.AddSongToFavoriteToUser(userId, song);
            if (!successAdded) return NotFound("Error adding the song");
            if (!await _repo.SaveAll()) return BadRequest("couldn't add song to favorite");
            return Ok("Song added");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveSongFromFavorites(int userId, int songId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var successRemoved = await _repo.RemoveFavoriteSongFromUser(userId, songId);
            if (!successRemoved) return NotFound("Error deleting the song");
            if (!await _repo.SaveAll()) return BadRequest();
            return Ok("Song removed");
        }
    }
}