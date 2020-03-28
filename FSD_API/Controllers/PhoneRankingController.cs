using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BO.StaticModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLB;
using ServiceLB.Helper;

namespace FSD_API.Controllers
{
    [Authorize(Roles = nameof(Role.Admin))]
    [Route("[controller]")]
    [ApiController]
    public class PhoneRankingController : ControllerBase
    {
        private readonly IPhoneRankingService _phoneRankingService;

        public PhoneRankingController(IPhoneRankingService phoneRankingService)
        {
            _phoneRankingService = phoneRankingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPhoneRanking(
            [FromQuery] string name,
            [FromQuery] string os,
            [FromQuery] int? page,
            [FromQuery] int? pagesize)

        {
            var userinformation = ClaimHelper.GetClaim(User.Identity as ClaimsIdentity);

            var result = await _phoneRankingService.GetPhoneRanking(userinformation.UserId, name, os, page, pagesize).ConfigureAwait(false);

            return Ok(result);
        }
    }
}