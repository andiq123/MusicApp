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

        public DownloadController(IDownloadRepository repo)
        {
            this._repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> Get(DownFileModel file)
        {
            if (file == null) BadRequest("No file was added");
            var fileFromServer = await _repo.downloadSongFromSource(file);
            return File(System.IO.File.OpenRead(fileFromServer), "audio/mpeg");
        }

    }
}