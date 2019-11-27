using System;

namespace back.Models
{
    public class Song
    {
        public string name { get; set; }
        public string artist { get; set; }
        public string album { get; set; }
        public string url { get; set; }
        public string cover_art_url { get; set; }
        public Playstate playstatus { get; set; } = new Playstate { playing = false, paused = false, stopped = true };
        public bool loading { get; set; }
    }
}