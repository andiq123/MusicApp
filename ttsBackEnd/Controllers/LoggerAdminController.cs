using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ttsBackEnd.Services;

namespace ttsBackEnd.Controllers
{
    public class LoggerAdminController
    {
        [Authorize]
        [ApiController]
        [Route("api/[controller]")]
        public class LoggerController : ControllerBase
        {
            private readonly ILoggerRepository _repo;
            private readonly IUserRepository _userRepository;

            public LoggerController(ILoggerRepository repo, IUserRepository userRepository)
            {
                this._repo = repo;
                this._userRepository = userRepository;
            }

            [HttpGet]
            public async Task<IActionResult> GetLogs()
            {
                var logs = await _repo.GetActivities();
                if (logs.Length == 0) return NotFound("No Logs Found");
                return Ok(logs);
            }

            [HttpGet("{userId}")]
            public async Task<IActionResult> GetLogsForUser(int userId)
            {
                var logs = await _repo.GetActivityForUser(userId);
                if (logs.Length == 0) return NotFound("No Logs Found");
                return Ok(logs);
            }

            [HttpGet("{userId}/favsongs")]
            public async Task<IActionResult> GetFavSongsForUser(int userId)
            {
                var logs = await _userRepository.GetUserFavSong(userId);
                if (logs == null || logs.Length == 0) return NotFound("No FavSongs Found");
                return Ok(logs);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteLog(int id)
            {
                var deletedLog = await _repo.DeleteActivity(id);
                if (!deletedLog) return NotFound("Log Not Found");
                return Ok("Log Deleted");
            }
        }
    }
}