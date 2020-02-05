using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using test.Models;
using ttsBackEnd.HubConfig;

namespace back.Services
{
    public class Download
    {
        private readonly string root = Path.Combine(System.Environment.CurrentDirectory, "wwwroot", "Uploads");
        private readonly IHubContext<StatusHub> _hub;

        public Download(IHubContext<StatusHub> hub)
        {
            _hub = hub;
            //Checks the directory, ensure existing
            if (!Directory.Exists(root)) Directory.CreateDirectory(root);
        }

        public async Task<string> downloadSongFromSource(DownFileModel file)
        {
            file.name = checkName(file.name);
            string destination = Path.Combine(root, file.name + ".mp3");
            if (File.Exists(destination)) return destination;
            WebClient wc = new WebClient();
            wc.DownloadProgressChanged += progressChanged;
            await wc.DownloadFileTaskAsync(new Uri(file.link), destination);
            return destination;
        }

        private async void progressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            await _hub.Clients.All.SendAsync("progressChanged", e.ProgressPercentage);
        }

        public string checkName(string name)
        {
            char[] badWords = new char[] { '$', '%', '&', '#', '@', '!', '|', '?', ' ', '_' };
            foreach (var word in badWords)
            {
                if (name.Contains(word)) name = name.Replace($"{word}", "");
            }
            return name;
        }
    }
}
