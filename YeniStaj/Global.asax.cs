using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using YeniStaj.Identity;
using YeniStaj.Models.IdentityModels;

namespace YeniStaj
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var roller = new String[]{"Admin","PoweredUser","User" };
            var roleManager = MembershipTools.NewRoleManager();
            foreach (var rol in roller)
            {
                if (!roleManager.RoleExists(rol))
                {
                    roleManager.Create(new Role()
                    { Name = rol
                    }) ;

                }
            }

        }
    }
}
