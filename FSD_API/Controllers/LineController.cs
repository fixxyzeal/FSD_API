using System;
using System.Collections.Generic;
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
    public class LineController : ControllerBase
    {
        private readonly ILineMessageService _lineMessageService;

        public LineController(ILineMessageService lineMessageService)
        {
            _lineMessageService = lineMessageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserProfile([FromQuery] string userid)

        {
            object result = await _lineMessageService.GetProfile(userid).ConfigureAwait(false);

            return Ok(result);
        }
    }
}