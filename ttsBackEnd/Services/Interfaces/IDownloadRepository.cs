using System.Threading.Tasks;
using ttsBackEnd.Models;

namespace ttsBackEnd.Services
{
    public interface IDownloadRepository
    {
        Task<string> downloadSongFromSource(DownFileModel file);
    }
}