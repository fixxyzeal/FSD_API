using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BO.ViewModels;
using BO.StaticModels;
using DAL;
using BO.Models;

namespace ServiceLB
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWorkService _unitOfWorkService;

        public AuthService(IConfiguration configuration
            , IUnitOfWorkService unitOfWorkService)
        {
            _configuration = configuration;
            _unitOfWorkService = unitOfWorkService;
        }

        public async Task<string> CreateAccessToken(Auth auth)
        {
            var user = await _unitOfWorkService.Service<User>()
                .GetAsync(x => (x.Email == auth.Email || x.UserName == auth.UserName) &&
                                x.Password == auth.Password).ConfigureAwait(false);
            if (user == null)
            {
                return "User or email or password not match";
            }

            return await GenerateJSONWebToken(auth).ConfigureAwait(false);
        }

        private async Task<string> GenerateJSONWebToken(Auth auth)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Email, auth.Email ),
                    new Claim(ClaimTypes.Email, auth.Email),
                    new Claim(ClaimTypes.Role, nameof(Role.Admin) ),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            var token = new JwtSecurityToken(
              _configuration["JWT:Issuer"],
              _configuration["JWT:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JWT:ExpireMin"])),
              signingCredentials: credentials);

            return await Task.Run(() => new JwtSecurityTokenHandler().WriteToken(token)).ConfigureAwait(false);
        }
    }
}