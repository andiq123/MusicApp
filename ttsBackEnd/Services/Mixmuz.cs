using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using back.Models;
using Microsoft.Extensions.Options;
using ttsBackEnd.Models;

namespace ttsBackEnd.Services
{
    public class Mixmuz
    {
        private readonly Scrapper _scrapper;
        private readonly IOptions<Sources> _config;
        public Mixmuz(IOptions<Sources> config)
        {
            _config = config;
            _scrapper = new Scrapper();
        }

        public async Task<IEnumerable<Song>> Get(string name)
        {
            IHtmlDocument document = await _scrapper.GetPage(_config.Value.MixmuzBaseUrl + name);
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
                    url = Url
                });
            }
            return songList;
        }
    }
}