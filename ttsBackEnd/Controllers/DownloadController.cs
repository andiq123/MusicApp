using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using back.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;
using test.Models;

using ttsBackEnd.HubConfig;
using ttsBackEnd.Services;

namespace test.Controllers
{
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