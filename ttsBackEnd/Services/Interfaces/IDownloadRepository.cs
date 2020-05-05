using System.Threading.Tasks;
using ttsBackEnd.Models;

namespace ttsBackEnd.Services
{
    public interface IDownloadRepository
    {
        Task<string> downloadSongFromSourceOld(FileDownload file);
        Task<byte[]> downloadSongFromSource(FileDownload file);
    }
}