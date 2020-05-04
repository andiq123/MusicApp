using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.Extensions.Options;
using ttsBackEnd.Models;

namespace ttsBackEnd.Services
{
    public class Mixmuz : IMixmuz
    {
        private readonly IScrapper _scrapper;
        private readonly IOptions<Sources> _config;

        public Mixmuz(IOptions<Sources> config, IScrapper scrapper)
        {
            this._scrapper = scrapper;
            this._config = config;
        }

        public async Task<IEnumerable<Song>> Get(string name)
        {
            IHtmlDocument document = await _scrapper.GetPage(_config.Value.MixmuzBaseUrl + name);
            if (document == null) return null;
            List<Song> songList = new List<Song>();
            IHtmlCollection<IElement> songs = document.QuerySelectorAll("div.item");
            await Task.Run(() =>
            {
                Parallel.ForEach<IElement>(songs, (song) =>
                    {
                        string title = song.QuerySelector("div.title span.t").InnerHtml;
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
                            Name = title,
                            Artist = artist,
                            Album = title,
                            Cover_art_url = coverArt,
                            Url = url
                        });
                    });
            });
            return songList;
        }
    }
}