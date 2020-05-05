using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ttsBackEnd.Models;
using ttsBackEnd.Services;

namespace ttsBackEnd.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SongController : ControllerBase
    {
        private readonly IMusicRepository _repo;
        private readonly ILoggerRepository _loggerRepository;

        public SongController(IMusicRepository repo, ILoggerRepository loggerRepository)
        {
            this._repo = repo;
            this._loggerRepository = loggerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Song>>> GetAsync([FromQuery]string name)
        {
            if (String.IsNullOrEmpty(name)) return BadRequest("No song name was given");
            var songs = await _repo.GetMusicAsync(name);
            if (songs == null) return StatusCode(500);
            if (songs.Count == 0) return NotFound();

            //log
            var log = new LogActivity();
            log.UserID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            log.Username = User.FindFirst(ClaimTypes.Name)?.Value;
            log.Description = $"{log.Username} has searched for: {name}";
            await _loggerRepository.LogActivity(log);

            return songs.ToArray();
        }


        //Old Version sync, takes longer
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<Song>>> Get([FromQuery]string name)
        // {
        //     var watch = new Stopwatch();
        //     watch.Start();
        //     var mixMuzSongs = await _mixMuz.Get(name);
        //     var muzFanSongs = await _muzFan.Get(name);
        //     var songs = new List<Song>();
        //     songs.AddRange(mixMuzSongs);
        //     songs.AddRange(muzFanSongs);
        //     watch.Stop();
        //     var ElapsedMilliseconds = watch.ElapsedMilliseconds;
        //     return Ok($"Sync, Elapsedm time: {ElapsedMilliseconds}");
        // }
    }
}