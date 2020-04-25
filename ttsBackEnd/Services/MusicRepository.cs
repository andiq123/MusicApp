using System.Collections.Generic;
using System.Threading.Tasks;
using ttsBackEnd.Models;

namespace ttsBackEnd.Services
{
    public class MusicRepository : IMusicRepository
    {
        private readonly IMuzfan _muzFan;
        private readonly IMixmuz _mixMuz;
        private readonly IUserRepository _repo;

        public MusicRepository(IMuzfan muzfan, IMixmuz mixmuz, IUserRepository repo)
        {
            this._muzFan = muzfan;
            this._mixMuz = mixmuz;
            this._repo = repo;
        }

        public async Task<ICollection<Song>> GetMusicAsync(string name)
        {
            List<Task<IEnumerable<Song>>> tasks = new List<Task<IEnumerable<Song>>>();
            tasks.Add(_muzFan.Get(name));
            tasks.Add(_mixMuz.Get(name));
            var results = await Task.WhenAll(tasks);
            List<Song> songs = new List<Song>();

            songs.AddRange(results[0]);
            songs.AddRange(results[1]);

            return songs.ToArray();
        }

    }
}