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
    public class BrushingInformationController : ControllerBase
    {
        private readonly IBrushingInformationService _brushingInformationService;

        public BrushingInformationController(IBrushingInformationService brushingInformationService)
        {
            _brushingInformationService = brushingInformationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()

        {
            var userinformation = ClaimHelper.GetClaim(User.Identity as ClaimsIdentity);

            var result = await _brushingInformationService
                .Get(x => x.UserId == userinformation.UserId.ToString())
                .ConfigureAwait(false);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> ManageInformation(BrushingInformation brushingInformation)

        {
            var userinformation = ClaimHelper.GetClaim(User.Identity as ClaimsIdentity);

            brushingInformation.UserId = userinformation.UserId.ToString();

            var result = await _brushingInformationService
                .ManageInformation(brushingInformation)
                .ConfigureAwait(false);

            return Ok(result);
        }
    }
}