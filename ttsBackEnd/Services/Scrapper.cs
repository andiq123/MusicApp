using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace ttsBackEnd.Services
{
    public class Scrapper : IScrapper
    {
        private readonly HttpClient _httpClient;
        private readonly IHtmlParser _parser;
        public Scrapper(HttpClient client)
        {
            this._httpClient = client;
            this._parser = new HtmlParser();
        }
        public async Task<IHtmlDocument> GetPage(string url)
        {
            using (HttpResponseMessage request = await _httpClient.GetAsync(new Uri(url), HttpCompletionOption.ResponseHeadersRead))
            {
                if (request.IsSuccessStatusCode)
                {
                    Stream respone = await request.Content.ReadAsStreamAsync();
                    IHtmlDocument document = _parser.ParseDocument(respone);
                    return document;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}