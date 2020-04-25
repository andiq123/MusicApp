using System.Threading.Tasks;
using test.Models;

namespace ttsBackEnd.Services
{
    public interface IDownloadRepository
    {
        Task<string> downloadSongFromSource(DownFileModel file);
    }
}