using System.Threading.Tasks;

namespace ServiceLB
{
    public interface IOilPriceService
    {
        Task SendOilFundPriceNoti();
    }
}