using Microsoft.AspNetCore.SignalR;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using test.Models;
using ttsBackEnd.HubConfig;
using ttsBackEnd.Services.Helpers;

namespace ttsBackEnd.Services
{

    public class DownloadRepository : IDownloadRepository
    {
        private readonly IHubContext<StatusHub> _hub;
        private readonly WebClient _client;

        public DownloadRepository(IHubContext<StatusHub> hub, WebClient client)
        {
            _hub = hub;
            _client = client;
            //Checks the directory, ensure existing
            Paths.Output.EnsureDirectoryExists();
        }

        public async Task<string> downloadSongFromSource(DownFileModel file)
        {
            file.Name.checkNameForBadChars();
            string downloadedFilePath = Path.Combine(Paths.Output, file.Name + ".mp3");
            if (downloadedFilePath.CheckFileExist()) return downloadedFilePath;
            _client.DownloadProgressChanged += progressChanged;
            await _client.DownloadFileTaskAsync(new Uri(file.Url), downloadedFilePath);
            _client.Dispose();
            return downloadedFilePath;
        }

        private async void progressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            await _hub.Clients.All.SendAsync("progressChanged", e.ProgressPercentage);
        }
    }
}
