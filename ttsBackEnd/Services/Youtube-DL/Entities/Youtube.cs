using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace back.Services.YoutubeDL.Entities
{
    public class Youtube
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Url { get; set; }

        public string Thumbnail { get; set; }

    }
}
