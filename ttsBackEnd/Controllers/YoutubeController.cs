using back.Services;
using back.Services.YoutubeDL;
using back.Services.YoutubeDL.Entities;
using back.Services.YoutubeDL.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class YoutubeController : ControllerBase
    {
        private readonly YtbConverter _ytbConverter;
        private Youtube _fileInfo;

        public YoutubeController()
        {
            _ytbConverter = new YtbConverter();
        }

        [HttpGet]
        public async Task<IActionResult> GetInfo(YoutubeDto file)
        {
            if (file == null) return BadRequest();
            _fileInfo = await _ytbConverter.GetInfo(file);
            return Ok(_fileInfo);
        }

        [HttpPost]
        public async Task<IActionResult> GetVideo(YoutubeDto file)
        {
            if (file == null) return BadRequest();
            if (_fileInfo == null) _fileInfo = await _ytbConverter.GetInfo(file);
            var convertedSongPath = await _ytbConverter.Convert(_fileInfo);
            if (!FileHelper.CheckExist(convertedSongPath)) return BadRequest("Something went bad in the server");
            return File(System.IO.File.OpenRead(convertedSongPath), "audio/mpeg");
        }
    }
}
