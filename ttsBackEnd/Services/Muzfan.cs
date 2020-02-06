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
                          url = Url
                      });
                  });

            });
            return songList;

        }
    }
}