using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using back.Models;
using test.SourcesHandler;

namespace back
{
    public class MuzFan : IWebScraper
    {
        private readonly string url;
        public MuzFan(string url)
        {
            this.url = url;
        }

        public async Task<List<Song>> GetSongs()
        {
            IHtmlDocument document = await Common.GetDocument(url);
            List<Song> songList = new List<Song>();
            var canzoni = document.QuerySelectorAll("div.track-item");
            foreach (var song in canzoni)
            {
                string Name = song.GetAttribute("no name");
                string Artist = song.GetAttribute("data-artist");
                string CoverArt;
                try
                {
                    CoverArt = song.GetAttribute("data-img");
                }
                catch
                {
                    CoverArt = $"https://icon-library.net/images/song-icon-png/song-icon-png-23.jpg";
                }
                string Url = song.GetAttribute("data-track");
                songList.Add(new Song
                {
                    name = Name,
                    artist = Artist,
                    album = Name,
                    cover_art_url = CoverArt,
                    url = Url,
                    loading = false
                });
            }
            return songList;
        }
        public async Task<bool> connectionTest()
        {
            IHtmlDocument document = await Common.GetDocument(url);
            var songCheck = document.QuerySelector("div.track-item").GetAttribute("data-artist");
            if (songCheck != null)
                return true;
            else return false;
        }
    }
}