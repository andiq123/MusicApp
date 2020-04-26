using System.Collections.Generic;
using System.Threading.Tasks;
using ttsBackEnd.Models;

namespace ttsBackEnd.Services
{
    public interface IMusicRepository
    {
        Task<ICollection<Song>> GetMusicAsync(string name);
    }

}