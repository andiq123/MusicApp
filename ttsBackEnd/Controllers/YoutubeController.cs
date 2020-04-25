using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ttsBackEnd.Services;
using ttsBackEnd.Services.Helpers;
using ttsBackEnd.Services.YoutubeDL.Entities;
using ttsBackEnd.Services.YoutubeDL.Models;

namespace ttsBackEnd.Controllers
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
            else if (_fileInfo == null) _fileInfo = await _ytbConverter.GetInfo(file);
            var convertedSongPath = await _ytbConverter.Convert(_fileInfo);
            if (!FileHelper.CheckFileExist(convertedSongPath)) return StatusCode(StatusCodes.Status500InternalServerError, "Something went bad in the server");
            return File(System.IO.File.OpenRead(convertedSongPath), "audio/mpeg");
        }
    }
}
