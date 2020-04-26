using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ttsBackEnd.Models;
using ttsBackEnd.Services;

namespace test.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DownloadController : ControllerBase
    {
        private readonly IDownloadRepository _repo;
        private readonly ILoggerRepository _loggerRepo;

        public DownloadController(IDownloadRepository repo, ILoggerRepository loggerRepo)
        {
            this._repo = repo;
            this._loggerRepo = loggerRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Get(DownFileModel file)
        {
            if (file == null) BadRequest("No file was added");
            var fileFromServer = await _repo.downloadSongFromSource(file);

            //log
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var log = new LogActivity();
            log.UserID = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            log.Username = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            log.Description = $"{log.Username} has downloaded the file: {file.Name}";
            await _loggerRepo.LogActivity(log);

            return File(System.IO.File.OpenRead(fileFromServer), "audio/mpeg");
        }

    }
}