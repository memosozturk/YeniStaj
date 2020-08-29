using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YeniStaj.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("Username")]
        
        public String Username { get; set; }
        [StringLength(32,MinimumLength =8,ErrorMessage ="Şifreniz en az 8,en çok 32 karakter olmalıdır!")]
        [DisplayName("Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}