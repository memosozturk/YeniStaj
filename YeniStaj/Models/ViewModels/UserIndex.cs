using System.ComponentModel.DataAnnotations;
using System.Web;

namespace YeniStaj.Models.ViewModels
{
    public class UserIndex
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

        public string PhoneNumber { get; set; }

    }
}