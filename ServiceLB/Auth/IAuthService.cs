using System.Threading.Tasks;
using BO.ViewModels;

namespace ServiceLB
{
    public interface IAuthService
    {
        Task<string> CreateAccessToken(Auth auth);
    }
}