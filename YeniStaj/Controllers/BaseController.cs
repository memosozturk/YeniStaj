using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YeniStaj.Identity;

namespace YeniStaj.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        
        protected List<SelectListItem> GetUserList()
        {
            var data = new List<SelectListItem>();
            MembershipTools.NewUserStore().Users
                .ToList()
                .ForEach(x =>
                {
                    data.Add(new SelectListItem()
                    {
                        Text = $"{x.Name} {x.Surname}",
                        Value = x.Id
                    });
                });
            return data;
        }
        protected List<SelectListItem> GetRoleList()
        {
            var data = new List<SelectListItem>();
            MembershipTools.NewRoleStore().Roles
                .ToList()
                .ForEach(x =>
                {
                    data.Add(new SelectListItem()
                    {
                        Text = $"{x.Name}",
                        Value = x.Id
                    });
                });
            return data;
        }
    }
}