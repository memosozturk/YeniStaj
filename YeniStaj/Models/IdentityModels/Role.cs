using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YeniStaj.Models.IdentityModels
{
    public class Role:IdentityRole
    {
        public Role()
        {

        }
        public Role(string description)
        {
            Description = description;

        }
        [StringLength(100)]
        public String Description { get; set; }
    }
}