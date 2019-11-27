using System.Collections.Generic;
using System.Threading.Tasks;
using back.Models;

namespace test.SourcesHandler
{
    public interface IWebScraper
    {
        Task<bool> connectionTest();
        Task<List<Song>> GetSongs();
    }
}