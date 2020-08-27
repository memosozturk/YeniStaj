using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YeniStaj.Models.ViewModels
{
    public class RegisterLoginViewModel
    {
        public LoginViewModel LoginViewModel { get; set; }
        public RegisterViewModel RegisterViewModel { get; set; }
    }
}