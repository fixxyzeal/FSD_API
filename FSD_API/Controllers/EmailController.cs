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
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(EmailViewModel email
            )

        {
            await _emailService.Send(email.To, email.Subject, email.Body, true).ConfigureAwait(false);

            return Ok();
        }
    }
}