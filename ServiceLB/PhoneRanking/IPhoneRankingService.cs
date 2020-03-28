using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BO.Models;

namespace ServiceLB
{
    public interface IPhoneRankingService
    {
        Task<IEnumerable<PhoneRanking>> GetPhoneRanking(Guid userid, string name, string os, int? page, int? pagesize);
    }
}