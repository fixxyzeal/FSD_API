using BO.Models.Mongo;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLB
{
    public class BrushingInformationService : IBrushingInformationService
    {
        private readonly IMongoUnitOfWork _mongoUnitOfWork;
        private readonly string collectionName = "BrushingInformation";

        public BrushingInformationService(IMongoUnitOfWork mongoUnitOfWork)
        {
            _mongoUnitOfWork = mongoUnitOfWork;
        }

        public async Task<IEnumerable<BrushingInformation>> Get(Expression<Func<BrushingInformation, bool>> match)
        {
            return await _mongoUnitOfWork.GetAllAsync(collectionName, match).ConfigureAwait(false);
        }

        public async Task<BrushingInformation> ManageInformation(BrushingInformation brushingInformation)
        {
            brushingInformation.BrushingDate = DateTime.Now.ToUniversalTime().Date;

            var data = await _mongoUnitOfWork
                .GetAsync<BrushingInformation>(collectionName, x => x.UserId == brushingInformation.UserId && x.BrushingDate == brushingInformation.BrushingDate)
                .ConfigureAwait(false);

            if (data != null)
            {
                if (data.BrushingRemain > 0)
                {
                    data.BrushingRemain--;
                }

                await _mongoUnitOfWork.UpdateAsync(collectionName, x => x.Id == brushingInformation.Id, data).ConfigureAwait(false);
            }
            else
            {
                brushingInformation.BrushingRemain = brushingInformation.BrushingSet;

                data = await _mongoUnitOfWork.CreateAsync(collectionName, brushingInformation).ConfigureAwait(false);
            }
            return data;
        }
    }
}