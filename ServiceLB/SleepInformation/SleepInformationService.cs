using BO.Models.Mongo;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLB
{
    public class SleepInformationService : ISleepInformationService
    {
        private readonly IMongoUnitOfWork _mongoUnitOfWork;
        private readonly string collectionName = "SleepInformation";

        public SleepInformationService(IMongoUnitOfWork mongoUnitOfWork)
        {
            _mongoUnitOfWork = mongoUnitOfWork;
        }

        public async Task<IEnumerable<SleepInformation>> Get(Expression<Func<SleepInformation, bool>> match)
        {
            return await _mongoUnitOfWork.GetAllAsync(collectionName, match).ConfigureAwait(false);
        }

        public async Task<SleepInformation> Add(SleepInformation sleepInformation)
        {
            sleepInformation.SleepTime = DateTime.Now.ToUniversalTime();

            return await _mongoUnitOfWork.CreateAsync(collectionName, sleepInformation).ConfigureAwait(false);
        }

        public async Task Update(SleepInformation sleepInformation)
        {
            await _mongoUnitOfWork.UpdateAsync(collectionName, x => x.Id == sleepInformation.Id, sleepInformation).ConfigureAwait(false);
        }

        public async Task Delete(string Id)
        {
            await _mongoUnitOfWork.RemoveAsync<SleepInformation>(collectionName, x => x.Id == Id).ConfigureAwait(false);
        }
    }
}