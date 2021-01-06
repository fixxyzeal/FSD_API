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
    public class FundsService : IFundsService
    {
        private readonly IWebScraper _webScraper;
        private readonly ILineMessageService _lineMessageService;
        private readonly IMongoUnitOfWork _mongoUnitOfWork;
        private readonly string collectionName = "SET";

        public FundsService
            (
                IWebScraper webScraper,
                ILineMessageService lineMessageService,
                IMongoUnitOfWork mongoUnitOfWork
            )
        {
            _webScraper = webScraper;
            _lineMessageService = lineMessageService;
            _mongoUnitOfWork = mongoUnitOfWork;
        }

        public async Task<IEnumerable<SET>> GetSET()
        {
            var data = await _mongoUnitOfWork.GetAllAsync<SET>(collectionName, _ => true).ConfigureAwait(false);
            return data;
        }

        public async Task SendSETNoti()
        {
            //Get Line User
            var data = await _mongoUnitOfWork.GetAllAsync<LineUsers>("LineUsers", x => true).ConfigureAwait(false);

            string[] users = data.Select(x => x.UserId).Distinct().ToArray();

            //Get SET Data
            var doc = await _webScraper.GetPageData("https://marketdata.set.or.th/mkt/sectorquotation.do").ConfigureAwait(false);

            if (doc.DocumentElement.OuterHtml.Length > 0)
            {
                List<AngleSharp.Dom.IElement> set_Today = doc.All.Where(m => m.LocalName == "td").ToList();
                //Save to database
                await SaveData(set_Today).ConfigureAwait(false);

                string text = $"ราคา SET วันนี้ {set_Today[1].TextContent}\n ราคา SET50 วันนี้ {set_Today[9].TextContent}\n ราคา SET100 วันนี้ {set_Today[17].TextContent}\n{StringHelper.GetCodePoint("0x100080")}";

                foreach (string user in users)
                {
                    //Send Line notification to user
                    await _lineMessageService.SendTextMessage(user, new string[] { text }).ConfigureAwait(false);
                }
            }
        }

        private async Task SaveData(List<AngleSharp.Dom.IElement> set_Today)
        {
            var today = DateTime.Now.ToUniversalTime().Date;

            //Insert SET
            SET set = new SET()
            {
                Date = today,
                IndexName = "SET",
                Index = double.Parse(set_Today[1].TextContent),
            };

            await _mongoUnitOfWork.CreateAsync(collectionName, set).ConfigureAwait(false);

            //Insert SET 50
            set = new SET()
            {
                Date = today,
                IndexName = "SET50",
                Index = double.Parse(set_Today[9].TextContent),
            };

            await _mongoUnitOfWork.CreateAsync(collectionName, set).ConfigureAwait(false);

            //Insert SET 100
            set = new SET()
            {
                Date = today,
                IndexName = "SET100",
                Index = double.Parse(set_Today[17].TextContent),
            };

            await _mongoUnitOfWork.CreateAsync(collectionName, set).ConfigureAwait(false);
        }
    }
}