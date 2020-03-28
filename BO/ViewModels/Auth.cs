using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BO.ViewModels
{
    public class Auth
    {
        [EmailAddress]
        public string Email { get; set; }

        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}