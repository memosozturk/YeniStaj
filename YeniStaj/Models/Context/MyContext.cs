using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using YeniStaj.Models.Entities;
using YeniStaj.Models.IdentityModels;


namespace YeniStaj.Models.Context
{
    public class MyContext:IdentityDbContext<User>
    {
        public MyContext()
        :base (nameOrConnectionString:"name=MyCon")
        {
            this.InstanceDate = DateTime.Now;
        }
        public DateTime InstanceDate { get; set; }
   
    
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
        public virtual DbSet<Project> Projects { get; set; }

    }

}