using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ttsBackEnd.Services;

namespace ttsBackEnd.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IFavsongRepository _songRepo;

        public UsersController(IUserRepository repo, IFavsongRepository songRepo)
        {
            this._repo = repo;
            this._songRepo = songRepo;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();
            if (users.Length == 0) return NotFound("No User found");
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _repo.GetUser(userId);
            if (user == null) return NotFound("No User found");
            return Ok(user);
        }

        [HttpGet("{userId}/favsongs")]
        public async Task<IActionResult> GetUserSongs(int userId)
        {
            var songs = await _songRepo.GetUserSong(userId);
            if (songs.Length == 0) return NotFound($"This user doesn't have any favorite songs");
            return Ok(songs);
        }
    }
}