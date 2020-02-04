using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace ttsBackEnd.Services
{
    public class Scrapper
    {
        private readonly HttpClient httpClient;
        public Scrapper()
        {
            httpClient = new HttpClient();
        }
        public async Task<IHtmlDocument> GetPage(string url)
        {
            HttpResponseMessage request = await httpClient.GetAsync(new Uri(url), HttpCompletionOption.ResponseHeadersRead);
            Stream respone = await request.Content.ReadAsStreamAsync();
            IHtmlDocument document = new HtmlParser().ParseDocument(respone);
            return document;
        }
    }
}