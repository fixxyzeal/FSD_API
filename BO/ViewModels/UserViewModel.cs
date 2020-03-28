using BO.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BO.ViewModels
{
    public class UserViewModel
    {
        public IEnumerable<User> Data { get; set; }

        public double Count { get; set; }
    }
}