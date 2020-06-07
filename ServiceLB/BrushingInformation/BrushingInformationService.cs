using BO.Models.Mongo;
using DAL;
using ServiceLB.Helper;
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
        private readonly ILineMessageService _lineMessageService;
        private readonly string collectionName = "BrushingInformation";

        public BrushingInformationService(IMongoUnitOfWork mongoUnitOfWork, ILineMessageService lineMessageService)
        {
            _mongoUnitOfWork = mongoUnitOfWork;
            _lineMessageService = lineMessageService;
        }

        public async Task<IEnumerable<BrushingInformation>> Get(bool? latest, string userId)
        {
            Expression<Func<BrushingInformation, bool>> match = (x) => x.UserId == userId;

            if (latest == true)
            {
                match = (x) => x.UserId == userId && x.BrushingDate == DateTime.Now.ToUniversalTime().Date;
            }

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

                await _mongoUnitOfWork.UpdateAsync(collectionName, x => x.Id == data.Id, data).ConfigureAwait(false);
            }
            else
            {
                brushingInformation.BrushingRemain = brushingInformation.BrushingSet - 1;
                data = await _mongoUnitOfWork.CreateAsync(collectionName, brushingInformation).ConfigureAwait(false);
            }
            return data;
        }

        public async Task DoWork()
        {
            var result = await _mongoUnitOfWork.GetAllAsync<BrushingInformation>(collectionName, x => x.BrushingDate == DateTime.Now.ToUniversalTime().Date).ConfigureAwait(false);
            string sendText = "";
            foreach (var item in result)
            {
                if (item.BrushingRemain == 0)
                {
                    continue;
                }

                if (item.BrushingRemain == item.BrushingSet)
                {
                    sendText = $"วันนี้คุณยังไม่ได้แปรงฟันเลย กรุณาแปรงฟันวันละ 3 ครั้งเพื่อสุขภาพฟันที่ดี {StringHelper.GetCodePoint("0x100083")}";
                }

                if (item.BrushingRemain > 0 && item.BrushingRemain != item.BrushingSet)
                {
                    sendText = $"คุณยังค้างแปรงฟันอีก {item.BrushingRemain} ครั้ง กรุณาแปรงฟันเพื่อสุขภาพฟันที่ดี {StringHelper.GetCodePoint("0x100082")}";
                }

                await _lineMessageService.SendTextMessage(item.LineUserId, new string[] { sendText }).ConfigureAwait(false);
            }
        }
    }
}