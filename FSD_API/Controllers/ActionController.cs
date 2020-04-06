using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BO.StaticModels;
using BO.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLB;
using ServiceLB.Helper;

namespace FSD_API.Controllers
{
    [Authorize(Roles = nameof(Role.Admin))]
    [Route("[controller]")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private readonly IActionService _actionService;

        public ActionController(IActionService actionService)
        {
            _actionService = actionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAction(
            [FromQuery] string displayname,
            [FromQuery] string userid,
            [FromQuery] string platform,
            [FromQuery] string message,
            [FromQuery] int? page,
            [FromQuery] int? pagesize)

        {
            var result = await _actionService.GetAction(userid, platform, displayname, message, page, pagesize).ConfigureAwait(false);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAction(
           [FromBody] UserActionViewModel model)

        {
            await _actionService.AddAction(model).ConfigureAwait(false);
            return Ok();
        }
    }
}