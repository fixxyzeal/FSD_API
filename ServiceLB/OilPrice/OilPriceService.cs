using AngleSharp.Dom;
using BO.Models.Mongo;
using DAL;
using ServiceLB.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLB
{
    public class OilPriceService : IOilPriceService
    {
        private readonly ICacheService _cacheService;
        private readonly IWebScraper _webScraper;
        private readonly ILineMessageService _lineMessageService;
        private readonly IMongoUnitOfWork _mongoUnitOfWork;

        public OilPriceService(
            ICacheService cacheService,
            IWebScraper webScraper,
            ILineMessageService lineMessageService,
            IMongoUnitOfWork mongoUnitOfWork)
        {
            _cacheService = cacheService;
            _webScraper = webScraper;
            _lineMessageService = lineMessageService;
            _mongoUnitOfWork = mongoUnitOfWork;
        }

        public async Task SendOilFundPriceNoti()
        {
            //Get Line User
            var data = await _mongoUnitOfWork.GetAllAsync<LineUsers>("LineUsers", x => true).ConfigureAwait(false);

            string[] users = data.Select(x => x.UserId).Distinct().ToArray();

            //Get Oil Price
            var doc = await _webScraper.GetPageData("https://www.kasikornasset.com/th/mutual-fund/fund-template/Pages/K-OIL.aspx").ConfigureAwait(false);

            if (doc.DocumentElement.OuterHtml.Length > 0)
            {
                var oil_List_Today = doc.All.Where(m => m.LocalName == "td" && m.ClassName == "td-val nowrap a-center v-middle").ToList();

                var diff = Convert.ToDouble(oil_List_Today[2].TextContent) - Convert.ToDouble(oil_List_Today[7].TextContent);

                string text = $"ราคากองทุน k-oil วันที่ {oil_List_Today[0].TextContent} \nราคา {oil_List_Today[1].TextContent} \nราคาแตกต่าง {diff:0.##} {StringHelper.GetCodePoint("0x100080")}";

                foreach (var user in users)
                {
                    await _lineMessageService.SendTextMessage(user, new string[] { text }).ConfigureAwait(false);
                }
            }
        }
    }
}