using back.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class YoutubeController : ControllerBase
    {
        [HttpPost]
        public ActionResult Get(YTArguments args)
        {
            var ytbConv = new YtbConverter();
            var convertedSongPath = ytbConv.ConvertSong(args);
            return File(System.IO.File.OpenRead(convertedSongPath), "audio/mpeg");
        }
    }
}
