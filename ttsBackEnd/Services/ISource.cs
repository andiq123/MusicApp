using System.Collections.Generic;
using System.Threading.Tasks;
using back.Models;

namespace ttsBackEnd.Services
{
    public interface ISource
    {
        Task<IEnumerable<Song>> Get(string name);
    }
}