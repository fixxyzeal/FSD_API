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
    public class OilPriceController : ControllerBase
    {
        private readonly IOilPriceService _oilPriceService;

        public OilPriceController(
            IOilPriceService oilPriceService
            )
        {
            _oilPriceService = oilPriceService;
        }

        [AllowAnonymous]
        [HttpPost("SendOilFundsLineNoti")]
        public async Task<IActionResult> SendLineNotification()

        {
            await _oilPriceService.SendOilFundPriceNoti().ConfigureAwait(false);

            Log.Information($"Send Line OilPriceNotification");

            return Ok();
        }
    }
}