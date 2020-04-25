using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ttsBackEnd.Models;
using ttsBackEnd.Services;

namespace ttsBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongController : ControllerBase
    {
        private readonly IMusicRepository _repo;
        public SongController(IMusicRepository repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Song>>> GetAsync([FromQuery]string name)
        {
            if (String.IsNullOrEmpty(name)) return BadRequest("No song name was given");
            var songs = await _repo.GetMusicAsync(name);
            if (songs == null) return StatusCode(500);
            if (songs.Count == 0) return NotFound();
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