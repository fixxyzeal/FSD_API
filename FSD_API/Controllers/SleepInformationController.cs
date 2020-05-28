using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BO.Models.Mongo;
using BO.StaticModels;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLB;
using ServiceLB.Helper;

namespace FSD_API.Controllers
{
    [Authorize(Roles = nameof(Role.Admin))]
    [Route("[controller]")]
    [ApiController]
    public class SleepInformationController : ControllerBase
    {
        private readonly ISleepInformationService _sleepInformationService;

        public SleepInformationController(ISleepInformationService sleepInformationService)
        {
            _sleepInformationService = sleepInformationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()

        {
            var userinformation = ClaimHelper.GetClaim(User.Identity as ClaimsIdentity);

            var result = await _sleepInformationService
                .Get(x => x.UserId == userinformation.UserId.ToString())
                .ConfigureAwait(false);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SleepInformation sleepInformation)

        {
            var userinformation = ClaimHelper.GetClaim(User.Identity as ClaimsIdentity);

            sleepInformation.UserId = userinformation.UserId.ToString();

            var result = await _sleepInformationService
                .Add(sleepInformation)
                .ConfigureAwait(false);

            return Ok(result);
        }
    }
}