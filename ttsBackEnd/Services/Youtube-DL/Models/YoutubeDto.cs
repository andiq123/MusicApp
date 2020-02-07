using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace back.Services.YoutubeDL.Models
{
    public class YoutubeDto
    {

        [Required(ErrorMessage = "Da linku treb eu sal scriu?")]
        public string Url { get; set; }
    }
}
