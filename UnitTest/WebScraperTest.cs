using NUnit.Framework;
using ServiceLB;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace UnitTest
{
    public class WebScraperTest
    {
        private IWebScraper webScraper;

        [SetUp]
        public void Setup()
        {
            webScraper = new WebScraper();
        }

        [Test, Order(1)]
        public async Task TestGetData()
        {
            var result = await webScraper.GetPageData("https://www.antutu.com/en/ranking/rank1.htm").ConfigureAwait(false);

            Assert.AreNotEqual(null, result);
        }
    }
}