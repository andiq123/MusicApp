using System;

namespace ttsBackEnd.Models
{
    public class Song
    {
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Url { get; set; }
        public string Cover_art_url { get; set; }
    }
}