using System.ComponentModel.DataAnnotations;
using System.Web;
using YeniStaj.Models.Entities;

namespace YeniStaj.Models.ViewModels
{
    public class UserProfileViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Ad")]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        [Required]
        [Display(Name = "Soyad")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Telefon No.")]
        [StringLength(11,ErrorMessage="Telefon no düzgün gir",MinimumLength =11)]
        public string PhoneNumber { get; set; }

        public string AvatarPath { get; set; }
        public HttpPostedFileBase PostedFile { get; set; }
        public Project ProjectUser { get; set; }
    }
}