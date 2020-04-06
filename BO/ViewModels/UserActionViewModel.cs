using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BO.ViewModels
{
    public class UserActionViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string UserDisplayName { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string Platform { get; set; }
    }
}