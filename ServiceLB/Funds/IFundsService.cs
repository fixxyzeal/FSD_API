using BO.Models.Mongo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLB
{
    public interface IFundsService
    {
        Task<IEnumerable<SET>> GetSET();

        Task SendSETNoti();
    }
}