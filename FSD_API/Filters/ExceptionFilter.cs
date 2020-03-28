using BO.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System.Net;
using System.Threading.Tasks;

namespace FSD_API.Filters
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        private readonly MessageResult _messageResult;

        public ExceptionFilter(MessageResult messageResult)
        {
            _messageResult = messageResult;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            // Write log the exception

            string exceptionMsg = string.Format("Controller:{0} | Action:{1} | Exception:{2}", context.RouteData.Values["controller"],
                context.RouteData.Values["action"], context.Exception.GetBaseException().ToString());

            Log.Error(exceptionMsg);

            _messageResult.Success = false;
            _messageResult.Message = exceptionMsg;

            await Task
                .Run(() => context.Result = new ObjectResult(_messageResult)
                { StatusCode = (int)HttpStatusCode.InternalServerError })
                .ConfigureAwait(false);
        }
    }
}