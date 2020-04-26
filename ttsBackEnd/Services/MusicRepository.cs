using System.Collections.Generic;
using System.Threading.Tasks;
using ttsBackEnd.Models;

namespace ttsBackEnd.Services
{
    public class MusicRepository : IMusicRepository
    {
        private readonly IMixmuz _mixMuz;
        private readonly IMuzfan _muzFan;

        private readonly IUserRepository _repo;

        public MusicRepository(IMixmuz mixmuz, IMuzfan muzfan, IUserRepository repo)
        {
            this._mixMuz = mixmuz;
            this._muzFan = muzfan;
            this._repo = repo;
        }

        public async Task<ICollection<Song>> GetMusicAsync(string name)
        {
            List<Task<IEnumerable<Song>>> tasks = new List<Task<IEnumerable<Song>>>();
            tasks.Add(_mixMuz.Get(name));
            tasks.Add(_muzFan.Get(name));
            var results = await Task.WhenAll(tasks);
            List<Song> songs = new List<Song>();

            songs.AddRange(results[0]);
            songs.AddRange(results[1]);

            return songs.ToArray();
        }

    }
}