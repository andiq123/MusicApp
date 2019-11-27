using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using test.Controllers;
using test.Models;

namespace back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MuzfanController : ControllerBase, IMusicController
    {
        [HttpGet]
        [Route("connTest")]
        public async Task<IActionResult> connectionTest()
        {
            return Ok(await new MuzFan(@"https://muzfan.net/?do=search&subaction=search&story=dua+lipa").connectionTest());
        }


        [HttpGet("{name}")]
        public async Task<IActionResult> GetSongs(string name)
        {
            return Ok(await new MuzFan($"https://muzfan.net/index.php?do=search&subaction=search&story={name}").GetSongs());
        }
    }
}