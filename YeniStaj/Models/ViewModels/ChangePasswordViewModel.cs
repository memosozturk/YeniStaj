using System.ComponentModel.DataAnnotations;

namespace YeniStaj.Models.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "Şifreniz en az 8 karakter olmalıdır!")]
        [Display(Name = "Eski Şifre")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "Şifreniz en az 8 karakter olmalıdır!")]
        [Display(Name = "Yeni Şifre")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre Tekrar")]
        [Compare("NewPassword", ErrorMessage = "Şifreler uyuşmuyor")]
        public string ConfirmNewPassword { get; set; }
    }
}