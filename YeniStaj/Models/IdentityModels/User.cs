using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YeniStaj.Models.IdentityModels
{
    public class User:IdentityUser
    {
        [StringLength(50)]
        [Required]
        public String Name { get; set; }
        [StringLength(50)]
        [Required]
        public String Surname { get; set; }
    }
}