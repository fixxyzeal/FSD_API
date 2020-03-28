using BO.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using ServiceLB;
using System.Threading.Tasks;

namespace FSD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(

            IAuthService authService
            )
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Auth auth)

        {
            string result = await _authService.CreateAccessToken(auth).ConfigureAwait(false);

            if (string.IsNullOrEmpty(result) || result.Contains("not match"))
            {
                Log.Error($"Login failed email: {auth.Email}");

                return Unauthorized(new { ErrorMessage = "Unauthorized" });
            }

            Log.Information($"Login Complete email: {auth.Email}");

            return Ok(new { Token = result });
        }
    }
}