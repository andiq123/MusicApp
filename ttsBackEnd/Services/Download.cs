using back.Services.Helpers;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using test.Models;
using ttsBackEnd.HubConfig;

namespace back.Services
{
    public class Download
    {
        private readonly IHubContext<StatusHub> _hub;

        public Download(IHubContext<StatusHub> hub)
        {
            _hub = hub;
            //Checks the directory, ensure existing
            FileHelper.EnsureDirectoryExists(Paths.Output);
        }

        public async Task<string> downloadSongFromSource(DownFileModel file)
        {
            file.Name = checkName(file.Name);
            string destination = Path.Combine(Paths.Output, file.Name + ".mp3");
            if (File.Exists(destination)) return destination;
            WebClient wc = new WebClient();
            wc.DownloadProgressChanged += progressChanged;
            await wc.DownloadFileTaskAsync(new Uri(file.Url), destination);
            return destination;
        }

        public async Task<byte[]> downloadSongStream(DownFileModel file)
        {
            WebClient wc = new WebClient();
            byte[] data = await wc.DownloadDataTaskAsync(file.Url);
            return data;
        }

        public async Task<string> downloadHttpMethod(DownFileModel file)
        {
            string Output = Paths.Output + file.Name + ".mp3";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(file.Url);
            httpWebRequest.Method = WebRequestMethods.Http.Get;
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream httpResponseStream = httpWebResponse.GetResponseStream();
            int bufferSize = 1024;
            byte[] buffer = new byte[bufferSize];
            int bytesRead = 0;
            FileStream fileStream = File.Create(Output);
            while ((bytesRead = await httpResponseStream.ReadAsync(buffer, 0, bufferSize)) != 0)
            {
                System.Console.WriteLine("miaw epta");
                await fileStream.WriteAsync(buffer, 0, bytesRead);
            }
            return Output;
        }

        private async void progressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            await _hub.Clients.All.SendAsync("progressChanged", e.ProgressPercentage);
        }

        public string checkName(string Name)
        {
            char[] badWords = new char[] { '$', '%', '&', '#', '@', '!', '|', '?', ' ', '_' };
            foreach (var word in badWords)
            {
                if (Name.Contains(word)) Name = Name.Replace($"{word}", "");
            }
            return Name;
        }
    }
}
