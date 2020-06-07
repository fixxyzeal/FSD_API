using System.Threading.Tasks;

namespace ServiceLB
{
    public interface ILineMessageService
    {
        Task SendTextMessage(string to, string[] messages);

        Task<object> GetProfile(string userid);
    }
}