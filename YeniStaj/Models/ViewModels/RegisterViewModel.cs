using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using YeniStaj.Models.Entities;

namespace YeniStaj.Models.ViewModels
{
    public class LoginRegisterViewModel
    {
        [Required]
        [DisplayName("Ad")]
        [StringLength(25)]
        public String Name { get; set; }
        [Required]
        [DisplayName("Soyad")]
        [StringLength(25)]
        public String Surname { get; set; }
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        [Required]
        [StringLength(32,MinimumLength =8,ErrorMessage ="Şifreniz 8 Karakterden Kısa 32 Karakterden Uzun olmamalıdır!")]
        [DisplayName("Şifre")]
        [DataType(DataType.Password)]
        public String Password { get; set; }
        [Required]
        [Compare("PassWord",ErrorMessage ="Şifreler Uyuşmuyor")]
        [DisplayName("Şifre Tekrar")]
        [DataType(DataType.Password)]
        public String ConfirmPassword { get; set; }
        [Required]
        [DisplayName("Kullanıcı Adı")]
        [StringLength(30,MinimumLength =5, ErrorMessage = "Kullanıcı adınız 5 Karakterden Kısa 30 Karakterden Uzun olmamalıdır!")]
        public String Username { get; set; }
      
    }
}