using BO.Models;
using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace ServiceLB
{
    public class PhoneRankingService : IPhoneRankingService
    {
        private readonly IUnitOfWorkService _unitOfWorkService;
        private readonly ICacheService _cacheService;
        private readonly IWebScraper _webScraper;

        public PhoneRankingService(
            IUnitOfWorkService unitOfWorkService,
            ICacheService cacheService,
            IWebScraper webScraper
            )
        {
            _cacheService = cacheService;
            _unitOfWorkService = unitOfWorkService;
            _webScraper = webScraper;
        }

        public async Task<IEnumerable<PhoneRanking>> GetPhoneRanking(Guid userid, string name, string os, int? page, int? pagesize)
        {
            if (!await CheckUpdateData(userid).ConfigureAwait(false))
            {
                var cache = await _cacheService.Get<IEnumerable<PhoneRanking>>(string.Concat("phoneRanking", name, os, page, pagesize)).ConfigureAwait(false);
                if (cache != null)
                {
                    return cache;
                }
            }
            // Get all data
            var query = _unitOfWorkService.Service<PhoneRanking>().GetQuery();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => EF.Functions.Like(x.DeviceName.ToLower(), $"%{ name.Trim().ToLower() }%"));
            }

            if (!string.IsNullOrEmpty(os))
            {
                query = query.Where(x => x.OS == os);
            }

            if (page.HasValue && pagesize.HasValue)
            {
                int? skip = (page - 1) * pagesize;

                query = query.Skip(skip.Value).Take(pagesize.Value);
            }

            var result = await query.OrderBy(x => x.Ranking).ToListAsync().ConfigureAwait(false);

            await _cacheService.Set(string.Concat("phoneRanking", name, os, page, pagesize), result, DateTime.Now.AddHours(6)).ConfigureAwait(false);

            return result;
        }

        private async Task<bool> CheckUpdateData(Guid userid)
        {
            var doc = await _webScraper.GetPageData("https://www.antutu.com/en/ranking/rank1.htm").ConfigureAwait(false);

            string cache = await _cacheService.Get<string>("phoneRankingRawHtml").ConfigureAwait(false);

            //Check update database if data not equal
            if (doc.DocumentElement.OuterHtml != cache)
            {
                DateTime rankingdate = DateTime.Now.Date;

                var ul_List = doc.All.Where(m => m.LocalName == "ul" && m.ClassName == "list-unstyled newrank-b").ToList();
                int i = 0;

                // Delete all phone ranking before insert

                var query = await _unitOfWorkService.Service<PhoneRanking>().GetAllAsync().ConfigureAwait(false);

                foreach (var item in query)
                {
                    _unitOfWorkService.Service<PhoneRanking>().Delete(item);
                }

                await _unitOfWorkService.SaveAsync().ConfigureAwait(false);

                foreach (var ul in ul_List)
                {
                    PhoneRanking phoneRanking = new PhoneRanking
                    {
                        Ranking = i++,
                        CreatedBy = userid
                    };

                    var liList = ul.QuerySelectorAll("li").ToArray();

                    phoneRanking.DeviceName = liList[0].TextContent;
                    phoneRanking.OS = "Andriod";
                    phoneRanking.CPU = Convert.ToInt32(liList[1].TextContent);
                    phoneRanking.GPU = Convert.ToInt32(liList[2].TextContent);
                    phoneRanking.MEM = Convert.ToInt32(liList[3].TextContent);
                    phoneRanking.UX = Convert.ToInt32(liList[4].TextContent);
                    phoneRanking.TotalScore = Convert.ToInt32(liList[5].TextContent);

                    PhoneRankingHistory phoneRankingHistory = new PhoneRankingHistory
                    {
                        Ranking = phoneRanking.Ranking,
                        DeviceName = phoneRanking.DeviceName,
                        OS = phoneRanking.OS,
                        CPU = phoneRanking.CPU,
                        GPU = phoneRanking.GPU,
                        MEM = phoneRanking.MEM,
                        UX = phoneRanking.UX,
                        TotalScore = phoneRanking.TotalScore,
                        RankingDate = rankingdate,
                        CreatedBy = userid
                    };

                    _unitOfWorkService.Service<PhoneRanking>().Add(phoneRanking);
                    _unitOfWorkService.Service<PhoneRankingHistory>().Add(phoneRankingHistory);
                }

                await _unitOfWorkService.SaveAsync().ConfigureAwait(false);

                // Set Cache
                await _cacheService.Set("phoneRankingRawHtml", doc.DocumentElement.OuterHtml, DateTime.Now.AddHours(6)).ConfigureAwait(false);

                return true;
            }

            return false;
        }
    }
}