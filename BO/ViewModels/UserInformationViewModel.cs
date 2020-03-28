using System;
using System.Collections.Generic;
using System.Text;

namespace BO.ViewModels
{
    public class UserInformationViewModel
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public Guid UserId { get; set; }

        public string Role { get; set; }
    }
}