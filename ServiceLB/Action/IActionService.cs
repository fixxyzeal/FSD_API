using System.Collections.Generic;
using System.Threading.Tasks;
using BO.Models;
using BO.ViewModels;

namespace ServiceLB
{
    public interface IActionService
    {
        Task AddAction(UserActionViewModel userAction);
        Task<IEnumerable<Action>> GetAction(string userid, string platform, string displayname, string message, int? page, int? pagesize);
    }
}