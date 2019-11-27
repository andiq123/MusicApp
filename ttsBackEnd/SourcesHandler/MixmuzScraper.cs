using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using back.Models;
using test.SourcesHandler;

namespace back
{

    public class MixmuzScraper : IWebScraper
    {
        private readonly string url;
        public MixmuzScraper(string url)
        {
            this.url = url;
        }
        public async Task<List<Song>> GetSongs()
        {
            IHtmlDocument document = await Common.GetDocument(url);
            List<Song> songList = new List<Song>();
            var canzoni = document.QuerySelectorAll("div.item");
            foreach (var song in canzoni)
            {
                string Name = song.QuerySelector("div.title span.t").InnerHtml;
                string Artist = song.QuerySelector("div.title span.a a").InnerHtml;
                string CoverArt;
                try
                {
                    CoverArt = song.QuerySelector("a.play img").GetAttribute("src");
                }
                catch
                {
                    CoverArt = $"https://icon-library.net/images/song-icon-png/song-icon-png-23.jpg";
                }
                string Url = song.QuerySelector("a.down").GetAttribute("href");
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
            var songCheck = document.QuerySelector("div.item a.play").GetAttribute("title");
            if (songCheck != null)
                return true;
            else return false;
        }
    }
}