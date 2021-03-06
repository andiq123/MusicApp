using System.IO;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ttsBackEnd.Dto;
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
        private readonly IMapper _mapper;

        public DownloadController(IDownloadRepository repo, ILoggerRepository loggerRepo, IMapper mapper)
        {
            this._repo = repo;
            this._loggerRepo = loggerRepo;
            this._mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Get(FileForDownloadDto fileFromUrl)
        {
            if (fileFromUrl == null || fileFromUrl.Url.Length <= 0 || fileFromUrl.Name.Length <= 0) BadRequest("No file was added");
            var file = _mapper.Map<FileDownload>(fileFromUrl);
            byte[] fileFromServer = await _repo.downloadSongFromSource(file);

            //log
            var log = new LogActivity();
            log.UserID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            log.Username = User.FindFirst(ClaimTypes.Name)?.Value;
            log.Description = $"{log.Username} has downloaded the file: {file.Name}";
            await _loggerRepo.LogActivity(log);

            Stream stream = new MemoryStream(fileFromServer);
            stream.Position = 0;
            return File(stream, "audio/mpeg", fileFromUrl.Name);
            // return File(System.IO.File.OpenRead(fileFromServer), "audio/mpeg");
        }

        //old
        [HttpPost("old")]
        public async Task<IActionResult> GetOld(FileForDownloadDto fileFromUrl)
        {
            if (fileFromUrl == null || fileFromUrl.Url.Length <= 0 || fileFromUrl.Name.Length <= 0) BadRequest("No file was added");
            var file = _mapper.Map<FileDownload>(fileFromUrl);
            var fileFromServer = await _repo.downloadSongFromSourceOld(file);

            //log
            var log = new LogActivity();
            log.UserID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            log.Username = User.FindFirst(ClaimTypes.Name)?.Value;
            log.Description = $"{log.Username} has downloaded the file: {file.Name}";
            await _loggerRepo.LogActivity(log);

            return File(System.IO.File.OpenRead(fileFromServer), "audio/mpeg");
        }

    }
}