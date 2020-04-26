using System.Collections.Generic;
using System.Threading.Tasks;
using ttsBackEnd.Models;

namespace ttsBackEnd.Services
{
    public interface IMixmuz
    {
        Task<IEnumerable<Song>> Get(string name);

    }
}