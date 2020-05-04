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
            // var fileFromServer = await _repo.downloadSongFromSource(file);

            //log
            var log = new LogActivity();
            log.UserID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            log.Username = User.FindFirst(ClaimTypes.Name)?.Value;
            log.Description = $"{log.Username} has downloaded the file: {file.Name}";
            await _loggerRepo.LogActivity(log);

            WebClient urlGrabber = new WebClient();
            byte[] data = urlGrabber.DownloadData(file.Url);
            FileStream fileStream = new FileStream(file.Name, FileMode.Open);

            fileStream.Write(data, 0, data.Length);
            fileStream.Seek(0, SeekOrigin.Begin);

            return (new FileStreamResult(fileStream, "audio/mpeg"));

            // return File(System.IO.File.OpenRead(fileFromServer), "audio/mpeg");
        }

    }
}