using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ttsBackEnd.Models;
using ttsBackEnd.Services;

namespace ttsBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusicController : ControllerBase
    {
        private readonly IOptions<Sources> _config;
        private readonly Mixmuz _mixMuz;
        private readonly Muzfan _muzFan;

        public MusicController(IOptions<Sources> config)
        {
            _config = config;
            _mixMuz = new Mixmuz(_config);
            _muzFan = new Muzfan(_config);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetAsync([FromQuery]string name)
        {
            List<Task<IEnumerable<Song>>> tasks = new List<Task<IEnumerable<Song>>>();
            tasks.Add(_mixMuz.Get(name));
            tasks.Add(_muzFan.Get(name));
            var results = await Task.WhenAll(tasks);
            List<Song> songs = new List<Song>();
            foreach (var item in results)
                songs.AddRange(item);
            return songs;
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