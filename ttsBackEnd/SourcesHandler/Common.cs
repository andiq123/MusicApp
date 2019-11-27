using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Microsoft.AspNetCore.SignalR;
using ttsBackEnd.HubConfig;

namespace test.SourcesHandler
{
    public static class Common
    {
        static IHubContext<BufferHub> _hub;
        public async static Task<IHtmlDocument> GetDocument(string url)
        {
            HttpResponseMessage request = await new HttpClient().GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            Stream respone = await request.Content.ReadAsStreamAsync();
            IHtmlDocument document = new HtmlParser().ParseDocument(respone);
            return document;
        }
        public static async Task<string> downloadSong(string name, string url, IHubContext<BufferHub> Hub)
        {
            _hub = Hub;
            string destination = Path.Combine(System.Environment.CurrentDirectory, "wwwroot", "Uploads", checkName(name) + ".mp3");
            if (File.Exists(destination)) return destination;
            else return await uploadSong(url, destination, Hub);
        }

        public static async Task<string> uploadSong(string url, string destination, IHubContext<BufferHub> Hub)
        {
            WebClient wc = new WebClient();
            wc.DownloadProgressChanged += progressChanged;
            await wc.DownloadFileTaskAsync(new Uri(url), destination);
            return destination;
        }

        private async static void progressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            await _hub.Clients.All.SendAsync("progressChanged", e.ProgressPercentage);
        }

        public static string checkName(string name)
        {
            char[] badWords = new char[] { '$', '%', '&', '#', '@', '!', '|', '?', ' ', '_' };
            foreach (var word in badWords)
            {
                if (name.Contains(word)) name = name.Replace($"{word}", "");
            }
            return name;
        }

        public static async Task<string> deleteOldSongs()
        {
            await Task.Run(() =>
              {
                  string[] files = Directory.GetFiles(Path.Combine(System.Environment.CurrentDirectory, "wwwroot", "Uploads"));
                  if (files.Length > 30)
                      foreach (var file in files)
                          if (new Random().Next(0, 2) == 1) File.Delete(file);
              });
            return "checked";
        }
    }
}

