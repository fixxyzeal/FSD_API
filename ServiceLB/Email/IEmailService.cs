using System.Threading.Tasks;

namespace ServiceLB
{
    public interface IEmailService
    {
        Task Send(string toAddress, string subject, string body, bool sendAsync = true);
    }
}