using System.Collections.Generic;
using System.Threading.Tasks;
using ttsBackEnd.Models;

namespace ttsBackEnd.Services
{
    public interface IMuzfan
    {
        Task<IEnumerable<Song>> Get(string name);
    }
}