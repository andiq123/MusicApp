using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using back.Models;
using Microsoft.Extensions.Options;
using ttsBackEnd.Models;

namespace ttsBackEnd.Services
{
    public class Muzfan : ISource
    {
        private readonly Scrapper _scrapper;
        private readonly IOptions<Sources> _options;

        public Muzfan(IOptions<Sources> options)
        {
            _options = options;
            _scrapper = new Scrapper();
        }

        public async Task<IEnumerable<Song>> Get(string name)
        {
            IHtmlDocument document = await _scrapper.GetPage(_options.Value.MuzfanBaseUrl + name);
            List<Song> songList = new List<Song>();
            IHtmlCollection<IElement> canzoni = document.QuerySelectorAll("div.track-item");
            await Task.Run(() =>
            {

                Parallel.ForEach<IElement>(canzoni, song =>
                  {
                      string name = song.GetAttribute("no name");
                      string artist = song.GetAttribute("data-artist");
                      string coverArt;
                      try
                      {
                          coverArt = song.GetAttribute("data-img");
                      }
                      catch
                      {
                          coverArt = $"https://icon-library.net/images/song-icon-png/song-icon-png-23.jpg";
                      }
                      string url = song.GetAttribute("data-track");
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