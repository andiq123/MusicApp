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
    [Route("api/[controller]")]
    public class FavsongController : ControllerBase
    {
        private readonly IFavsongRepository _repo;
        private readonly int _userId;

        public FavsongController(IFavsongRepository repo)
        {
            this._repo = repo;
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            this._userId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentUserFavSongs()
        {
            var songs = await _repo.GetUserSong(_userId);
            if (songs.Length == 0) return NotFound("Empty favorite song library");
            return Ok(songs);
        }

        [HttpPost]
        public async Task<IActionResult> AddSongToFavorite(Song song)
        {
            var successAdded = await _repo.AddSongToFavoriteToUser(_userId, song);
            if (!successAdded) return NotFound("Error adding the song");
            return Ok("Song added");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveSongFromFavorites(int id)
        {
            var successRemoved = await _repo.RemoveFavoriteSongFromUser(id, _userId);
            if (!successRemoved) return NotFound("Error deleting the song");
            return Ok("Song removed");
        }
    }
}