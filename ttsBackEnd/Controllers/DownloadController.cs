using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;
using test.Models;
using test.SourcesHandler;
using ttsBackEnd.HubConfig;

namespace test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DownloadController : ControllerBase
    {
        private readonly IHubContext<BufferHub> _hub;

        public DownloadController(IHubContext<BufferHub> hub)
        {
            _hub = hub;
        }

        [HttpPost]
        [Route("song")]
        public async Task<IActionResult> GetSong(DownFileModel file)
        {
            if (file == null) BadRequest("No file was added");
            return File(System.IO.File.OpenRead(await Common.downloadSong(file.name, file.link, _hub)), "audio/mpeg");
        }
    }
}