using BO.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ServiceLB.Helper
{
    public static class ClaimHelper
    {
        public static UserInformationViewModel GetClaim(ClaimsIdentity identity)
        {
            return new UserInformationViewModel
            {
                UserId = Guid.Parse(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                Email = identity.FindFirst(ClaimTypes.Email)?.Value,
                Role = identity.FindFirst(ClaimTypes.Role)?.Value,
                UserName = identity.FindFirst(ClaimTypes.Name)?.Value
            };
        }
    }
}