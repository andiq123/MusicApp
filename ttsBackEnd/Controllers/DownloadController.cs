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
            WebClient wc = new WebClient();
            wc.DownloadProgressChanged += progressChanged;
            byte[] data = await wc.DownloadDataTaskAsync(new Uri(file.link));
            StreamContent stream = new StreamContent(new MemoryStream(data));
            return File(await stream.ReadAsByteArrayAsync(), "audio/mpeg");
        }

        private async void progressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            await _hub.Clients.All.SendAsync("progressChanged", e.ProgressPercentage, e.BytesReceived);
        }

        public static string checkName(string name)
        {
            char[] badWords = new char[] { '$', '%', '&', '#', '@', '!', '|', '?', ' ', '_' };
            foreach (var word in badWords)
            {
                if (name.Contains(word)) name = name.Replace($"{word}", "");
            }
            return name;
        }

        [HttpPost]
        [Route("img")]
        public async Task<IActionResult> GetImg(DownFileModel file)
        {
            if (file == null) return BadRequest("No body found");
            byte[] dataImg = await new WebClient().DownloadDataTaskAsync(new Uri(file.link));
            var content = new StreamContent(new MemoryStream(dataImg));
            return File(await content.ReadAsByteArrayAsync(), "image/jpeg");
        }
    }
}