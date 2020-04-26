using System.Threading.Tasks;
using AngleSharp.Html.Dom;

namespace ttsBackEnd.Services
{
    public interface IScrapper
    {
        Task<IHtmlDocument> GetPage(string url);
    }
}