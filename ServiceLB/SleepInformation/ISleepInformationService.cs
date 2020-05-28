using BO.Models.Mongo;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ServiceLB
{
    public interface ISleepInformationService
    {
        Task<SleepInformation> Add(SleepInformation sleepInformation);

        Task Delete(string Id);

        Task<IEnumerable<SleepInformation>> Get(Expression<Func<SleepInformation, bool>> match);

        Task Update(SleepInformation sleepInformation);
    }
}