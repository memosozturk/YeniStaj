using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using YeniStaj.Models.IdentityModels;

namespace YeniStaj.Models.ViewModels
{
    public class ProjectIndexViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public String ProjeAdi { get; set; }
        public String ProjeAciklama { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public List<User> ProjectUser { get; set; }

    }
}