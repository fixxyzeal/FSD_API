using DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BO.Models;
using BO.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ServiceLB
{
    public class ActionService : IActionService
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public ActionService(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        public async Task AddAction(UserActionViewModel userAction)
        {
            _unitOfWorkService.Service<BO.Models.Action>().Add(new BO.Models.Action
            {
                UserId = userAction.UserId,
                Platform = userAction.Platform,
                UserDisplayName = userAction.UserDisplayName,
                Message = userAction.Message,
                ActionDate = DateTime.Now.Date
            });

            await _unitOfWorkService.SaveAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<BO.Models.Action>> GetAction(string userid, string platform, string displayname, string message, int? page, int? pagesize)
        {
            var query = _unitOfWorkService.Service<BO.Models.Action>().GetQuery();

            if (!string.IsNullOrEmpty(userid))
            {
                query = query.Where(x => x.UserId.Contains(userid));
            }

            if (!string.IsNullOrEmpty(platform))
            {
                query = query.Where(x => x.Platform.Contains(platform));
            }

            if (!string.IsNullOrEmpty(displayname))
            {
                query = query.Where(x => x.UserDisplayName.Contains(displayname));
            }

            if (!string.IsNullOrEmpty(message))
            {
                query = query.Where(x => x.Message.Contains(message));
            }

            if (page.HasValue && pagesize.HasValue)
            {
                query = _unitOfWorkService.Service<BO.Models.Action>().GetQueryPaging(query, page.Value, pagesize.Value);
            }

            return await query.OrderByDescending(x => x.CreatedDate).ToListAsync().ConfigureAwait(false);
        }
    }
}