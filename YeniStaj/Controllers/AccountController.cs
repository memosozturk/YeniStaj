using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using YeniStaj.Helpers;
using YeniStaj.Models.Context;
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
            if (HttpContext.GetOwinContext().Authentication.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(RegisterLoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Index",model);

                }
                var userManager = NewUserManager();
                var user =await  userManager.FindAsync(model.LoginViewModel.Username,model.LoginViewModel.Password);
                if (user==null)
                {
                    ModelState.AddModelError("","Kullanıcı Adı veya Şifre Hatalı");
                    ViewBag.Uyari = "Kullanıcı Adı veya Şifre Yanlış";
                    return View("Login",model);

                }
                var autManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = await userManager.CreateIdentityAsync(user,DefaultAuthenticationTypes.ApplicationCookie);
                autManager.SignIn(new AuthenticationProperties()
                {
                    IsPersistent = model.LoginViewModel.RememberMe
                }, userIdentity);
                
                return RedirectToAction("Index","Home");
            }
            catch (Exception ex)
            {

                TempData["Model"] = new ErrorViewModel()
                {
                    Text = $"Bir hata oluştu {ex.Message}",
                    ActionName = "Index",
                    ControllerName = "Account",
                    ErrorCode = 500
                };
                return RedirectToAction("Error", "Home");
            }
           
        }
        [HttpGet]
        public ActionResult Logout()
        {
            var autManager = HttpContext.GetOwinContext().Authentication;
            autManager.SignOut();
            return RedirectToAction("Login");
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
            if (ModelState.IsValid)
            {
                return View("Login",model);
            }
            try
            {
                var userManager = NewUserManager();
                var userStore = NewUserStore();

                var rm = model.RegisterViewModel;

                var user = await userManager.FindByNameAsync(rm.Username);
                if (user!=null)
            {
                ModelState.AddModelError("Username", "Bu mail adresi sistemde kayıtlı");
                return View("Login",model);
            }
                var newUser = new User()
                { 
                    UserName = rm.Username,
                    Name = rm.Name,
                    Surname = rm.Surname,
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
                TempData["Model"] = new ErrorViewModel()
                {
                    Text = $"Bir hata oluştu {ex.Message}",
                    ActionName = "Index",
                    ControllerName = "Account",
                    ErrorCode = 500
                };
                return RedirectToAction("Error", "Home");


            }
        }

        //Profil Sayfası
        [HttpGet]
        [Authorize]
        public ActionResult UserProfile()
        {
            try
            {
                var id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
                var user = NewUserManager().FindById(id);
                var data = new ProfilePasswordViewModel()
                {
                    UserProfileViewModel = new UserProfileViewModel()
                    {
                        Email = user.Email,
                        Id = user.Id,
                        Name = user.Name,
                        PhoneNumber = user.PhoneNumber,
                        Surname = user.Surname,
                        UserName = user.UserName,
                        AvatarPath = string.IsNullOrEmpty(user.AvatarPath) ? "/assets/img/avatars/avatar3.jpg" : user.AvatarPath
                    }
                };
                return View(data);
            }
            catch (Exception ex)
            {
                TempData["Model"] = new ErrorViewModel()
                {
                    Text = $"Bir hata oluştu {ex.Message}",
                    ActionName = "UserProfile",
                    ControllerName = "Account",
                    ErrorCode = 500
                };
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> UpdateProfile(ProfilePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("UserProfile", model);
            }

            try
            {
                var userManager = NewUserManager();
                var user = await userManager.FindByIdAsync(model.UserProfileViewModel.Id);

                user.Name = model.UserProfileViewModel.Name;
                user.Surname = model.UserProfileViewModel.Surname;
                user.PhoneNumber = model.UserProfileViewModel.PhoneNumber;
                if (user.Email != model.UserProfileViewModel.Email)
                {
                    //todo tekrar aktivasyon maili gönderilmeli. rolü de aktif olmamış role çevrilmeli.
                }
                user.Email = model.UserProfileViewModel.Email;

                if (model.UserProfileViewModel.PostedFile != null &&
                    model.UserProfileViewModel.PostedFile.ContentLength > 0)
                {
                    var file = model.UserProfileViewModel.PostedFile;
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string extName = Path.GetExtension(file.FileName);
                    fileName = StringHelpers.UrlFormatConverter(fileName);
                    fileName += StringHelpers.GetCode();
                    var klasoryolu = Server.MapPath("~/Upload/");
                    var dosyayolu = Server.MapPath("~/Upload/") + fileName + extName;

                    if (!Directory.Exists(klasoryolu))
                        Directory.CreateDirectory(klasoryolu);
                    file.SaveAs(dosyayolu);

                    WebImage img = new WebImage(dosyayolu);
                    img.Resize(250, 250, false);
                    img.AddTextWatermark("Wissen");
                    img.Save(dosyayolu);
                    var oldPath = user.AvatarPath;
                    user.AvatarPath = "/Upload/" + fileName + extName;

                    System.IO.File.Delete(Server.MapPath(oldPath));
                }


                await userManager.UpdateAsync(user);
                TempData["Message"] = "Güncelleme işlemi başarılı";
                return RedirectToAction("UserProfile");
            }
            catch (Exception ex)
            {
                TempData["Model"] = new ErrorViewModel()
                {
                    Text = $"Bir hata oluştu {ex.Message}",
                    ActionName = "UserProfile",
                    ControllerName = "Account",
                    ErrorCode = 500
                };
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> ChangePassword(ProfilePasswordViewModel model)
        {
            try
            {
                var userManager = NewUserManager();
                var id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
                var user = NewUserManager().FindById(id);
                var data = new ProfilePasswordViewModel()
                {
                    UserProfileViewModel = new UserProfileViewModel()
                    {
                        Email = user.Email,
                        Id = user.Id,
                        Name = user.Name,
                        PhoneNumber = user.PhoneNumber,
                        Surname = user.Surname,
                        UserName = user.UserName
                    }
                };
                model.UserProfileViewModel = data.UserProfileViewModel;
                if (!ModelState.IsValid)
                {
                    model.ChangePasswordViewModel = new ChangePasswordViewModel();
                    return View("UserProfile", model);
                }


                var result = await userManager.ChangePasswordAsync(
                    HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId(),
                    model.ChangePasswordViewModel.OldPassword, model.ChangePasswordViewModel.NewPassword);

                if (result.Succeeded)
                {
                    //todo kullanıcıyı bilgilendiren bir mail atılır
                    return RedirectToAction("Logout", "Account");
                }
                else
                {
                    var err = "";
                    foreach (var resultError in result.Errors)
                    {
                        err += resultError + " ";
                    }
                    ModelState.AddModelError("", err);
                    model.ChangePasswordViewModel = new ChangePasswordViewModel();
                    return View("UserProfile", model);
                }
            }
            catch (Exception ex)
            {
                TempData["Model"] = new ErrorViewModel()
                {
                    Text = $"Bir hata oluştu {ex.Message}",
                    ActionName = "UserProfile",
                    ControllerName = "Account",
                    ErrorCode = 500
                };
                return RedirectToAction("Error", "Home");
            }
        }

    }
}