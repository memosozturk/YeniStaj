using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using YeniStaj.Models.IdentityModels;
using YeniStaj.Models.ViewModels;
using static YeniStaj.Identity.MembershipTools;

namespace YeniStaj.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Register(RegisterLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login",model);
            }
            try
            {
                var userManager = NewUserManager();
                var roleManager = NewRoleManager();
                var userStore = NewUserStore();

                var rm = model.RegisterViewModel;

                var user = await userManager.FindByNameAsync(rm.Username);
                if (user!=null)
            {
                ModelState.AddModelError("Email", "Bu mail adresi sistemde kayıtlı");
                return View("Index",model);
            }
                var newUser = new User()
                {
                    Name = rm.Name,
                    Surname = rm.Surname,
                    UserName = rm.Username,
                    Email = rm.Email

                };
               var result=await userManager.CreateAsync(newUser,rm.Password);
                if (result.Succeeded)
                {
                    if (userStore.Users.Count()==1)
                    {
                      await userManager.AddToRoleAsync(newUser.Id, "Admin");
                       
                     }
                    else
                    {
                      await userManager.AddToRoleAsync(newUser.Id, "User");
                    }
                    //todo kullanıcıya mail gönderilsin.


                }
                else
                {
                    var err = " ";
                    foreach (var resultError in result.Errors)
                    {
                        err += resultError + "";
                    }
                    ModelState.AddModelError("", err);
                    return View("Index", model);
                }
                return RedirectToAction("Login");


            }
            catch (Exception ex)
            {
                return RedirectToAction("Login");

              
            }
        }
    }
}