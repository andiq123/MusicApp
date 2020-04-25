using System.ComponentModel.DataAnnotations;

namespace ttsBackEnd.Services.YoutubeDL.Entities
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
