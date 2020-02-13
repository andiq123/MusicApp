using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using back.Models;
using Microsoft.Extensions.Options;
using ttsBackEnd.Models;

namespace ttsBackEnd.Services
{
    public class Mixmuz : ISource
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
            IHtmlCollection<IElement> canzoni = document.QuerySelectorAll("div.item");
            await Task.Run(() =>
            {
                Parallel.ForEach<IElement>(canzoni, (song) =>
                    {
                        string name = song.QuerySelector("div.title span.t").InnerHtml;
                        string artist = song.QuerySelector("div.title span.a a").InnerHtml;
                        string coverArt;
                        try
                        {
                            coverArt = song.QuerySelector("a.play img").GetAttribute("src");
                        }
                        catch
                        {
                            coverArt = $"https://icon-library.net/images/song-icon-png/song-icon-png-23.jpg";
                        }
                        string url = "https:" + song.QuerySelector("a.down").GetAttribute("href");
                        songList.Add(new Song
                        {
                            Name = name,
                            Artist = artist,
                            Album = name,
                            Cover_art_url = coverArt,
                            Url = url
                        });
                    });
            });
            return songList;
        }
    }
}