using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ttsBackEnd.Services;

namespace ttsBackEnd.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users/[controller]")]
    public class LoggerController : ControllerBase
    {
        private readonly ILoggerRepository _repo;
        private readonly int _userId;

        public LoggerController(ILoggerRepository repo)
        {
            this._repo = repo;
            _userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
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
            if (userId != _userId)
                return Unauthorized();
            var logs = await _repo.GetActivityForUser(userId);
            if (logs.Length == 0) return NotFound("No Logs Found");
            return Ok(logs);
        }

        [HttpDelete("{userid}/{logId}")]
        public async Task<IActionResult> DeleteLog(int userId, int logId)
        {
            if (userId != _userId)
                return Unauthorized();
            var deletedLog = await _repo.DeleteActivity(logId);
            if (!deletedLog) return NotFound("Log Not Found");
            if (!await _repo.SaveAll()) return BadRequest("Happend an error");
            return Ok("Log Deleted");
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetLoggedSongsForUser(int userId)
        {
            if (userId != _userId)
                return Unauthorized();
            var songs = await _repo.GetLoggedSongsForUser(userId);
            if (songs == null) return NotFound("No Logs Found");
            return Ok(songs);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> DeleteLoggedSong(int userId, int logSongId)
        {
            if (userId != _userId)
                return Unauthorized();
            await _repo.DeleteSong(logSongId);
            if (!await _repo.SaveAll()) return BadRequest("Happend an error");
            return Ok("Logged Song delete");
        }
    }
}