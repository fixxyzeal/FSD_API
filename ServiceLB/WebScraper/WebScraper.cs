using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLB
{
    public class WebScraper : IWebScraper
    {
        public WebScraper()
        {
        }

        public async Task<IDocument> GetPageData(string url)
        {
            //Use the default configuration for AngleSharp
            var config = Configuration.Default.WithDefaultLoader();

            //Create a new context for evaluating webpages with the given config
            var context = BrowsingContext.New(config);

            HttpClient httpClient = new HttpClient();

            string htmlstring = await httpClient.GetStringAsync(url).ConfigureAwait(false);

            return await context.OpenAsync(x => x.Content(htmlstring)).ConfigureAwait(false);
        }
    }
}