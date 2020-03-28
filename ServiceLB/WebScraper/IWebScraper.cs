using System.Threading.Tasks;
using AngleSharp.Dom;

namespace ServiceLB
{
    public interface IWebScraper
    {
        Task<IDocument> GetPageData(string url);
    }
}