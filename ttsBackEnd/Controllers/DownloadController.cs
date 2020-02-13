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

namespace test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DownloadController : ControllerBase
    {
        private readonly Download _download;
        private Stopwatch _watch;

        public DownloadController(IHubContext<StatusHub> hub)
        {
            _download = new Download(hub);
            _watch = new Stopwatch();
        }

        [HttpPost]
        public async Task<IActionResult> Get(DownFileModel file)
        {
            if (file == null) BadRequest("No file was added");
            _watch.Start();
            var fileFromServer = await _download.downloadSongFromSource(file);
            _watch.Stop();
            return Ok($"Normal Method Time Elapsed: {_watch.ElapsedMilliseconds}");
            // return File(System.IO.File.OpenRead(fileFromServer), "audio/mpeg");

        }

        [HttpPost("stream")]
        public async Task<IActionResult> GetStream(DownFileModel file)
        {
            if (file == null) BadRequest("No file was added");
            _watch.Start();
            byte[] fileFromServer = await _download.downloadSongStream(file);
            _watch.Stop();
            return Ok($"Stream Method Time Elapsed: {_watch.ElapsedMilliseconds}");
            // return File(fileFromServer, "audio/mpeg");
        }

        [HttpPost("new")]
        public async Task<IActionResult> GetNew(DownFileModel file)
        {
            if (file == null) BadRequest("No file was added");
            _watch.Start();
            string fileFromServer = await _download.downloadHttpMethod(file);
            _watch.Stop();
            return Ok($"Stream Method Time Elapsed: {_watch.ElapsedMilliseconds}");
            // return File(fileFromServer, "audio/mpeg");
        }
    }
}