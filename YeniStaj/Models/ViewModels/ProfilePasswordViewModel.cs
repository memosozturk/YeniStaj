using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YeniStaj.Models.ViewModels
{
    public class ProfilePasswordViewModel
    {
        public UserProfileViewModel UserProfileViewModel { get; set; }
        public ChangePasswordViewModel ChangePasswordViewModel { get; set; }
    }
}