using BO.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using ServiceLB;
using System.Threading.Tasks;

namespace FSD_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FundsController : ControllerBase
    {
        private readonly IFundsService _fundsService;

        public FundsController(
            IFundsService fundsService
            )
        {
            _fundsService = fundsService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetFundsData()

        {
            //Get fundsdata
            var result = await _fundsService
                                  .GetSET()
                                  .ConfigureAwait(false);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("SendSetLineNoti")]
        public async Task<IActionResult> SendLineNotification()

        {
            await _fundsService.SendSETNoti().ConfigureAwait(false);

            Log.Information($"Send Line SET Notification");

            return Ok();
        }
    }
}