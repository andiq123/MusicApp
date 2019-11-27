using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using test.Controllers;
using test.Models;
using test.SourcesHandler;

namespace back.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MixmuzController : ControllerBase, IMusicController
    {
        [HttpGet("{name}")]
        public async Task<IActionResult> GetSongs(string name)
        {
            return Ok(await new MixmuzScraper($"https://mixmuz.ru/mp3/{name}").GetSongs());
        }

        [HttpGet]
        [Route("connTest")]
        public async Task<IActionResult> connectionTest()
        {
            await Common.deleteOldSongs();
            return Ok(await new MixmuzScraper(@"https://mixmuz.ru/mp3/dua%20lipa").connectionTest());
        }

    }
}