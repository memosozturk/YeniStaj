using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using YeniStaj.Helpers;
using YeniStaj.Models.Entities;
using YeniStaj.Models.IdentityModels;
using YeniStaj.Models.ViewModels;
using static YeniStaj.Identity.MembershipTools;

namespace YeniStaj.Controllers
{
    [Authorize(Roles = "Admin,PoweredUser")]
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult Index()
        {
          
            return View(NewUserManager().Users.ToList());
        }
        public ActionResult Export()
        {
            return View(NewUserManager().Users.ToList());
        }
        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        

        [HttpGet]
        public ActionResult EditUser(string id)
        {
            try
            {
                var user = NewUserManager().FindById(id);
                if (user == null)
                    return RedirectToAction("Index");

                var roller = GetRoleList();
                foreach (var role in user.Roles)
                {
                    foreach (var selectListItem in roller)
                    {
                        if (selectListItem.Value == role.RoleId)
                            selectListItem.Selected = true;
                    }
                }
                ViewBag.ProjeList = GetProjectSelectList();

                ViewBag.RoleList = roller;



                var model = new UserProfileViewModel()
                {
                    AvatarPath = user.AvatarPath,
                    Name = user.Name,
                    Email = user.Email,
                    Surname = user.Surname,
                    Id = user.Id,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName,
                    ProjectUser=user.ProjectUser,
                    Projeid=user.Projeid
                    
                };
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Model"] = new ErrorViewModel()
                {
                    Text = $"Bir hata oluştu {ex.Message}",
                    ActionName = "Index",
                    ControllerName = "Admin",
                    ErrorCode = 500
                };
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(UserProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                
                
                var userManager = NewUserManager();
                var user = await userManager.FindByIdAsync(model.Id);

                user.Name = model.Name;
                user.Surname = model.Surname;
                user.PhoneNumber = model.PhoneNumber;
                user.Email = model.Email;
                user.Projeid = model.Projeid;
                user.ProjectUser = model.ProjectUser;

                if (model.PostedFile != null &&
                    model.PostedFile.ContentLength > 0)
                {
                    var file = model.PostedFile;
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
                user.AvatarPath = model.AvatarPath;
                await userManager.UpdateAsync(user);
                ViewBag.ProjeList = GetProjectSelectList();
                TempData["Message"] = "Güncelleme işlemi başarılı";
                return RedirectToAction("EditUser", new { id = user.Id });
            }
            catch (Exception ex)
            {
                TempData["Model"] = new ErrorViewModel()
                {
                    Text = $"Bir hata oluştu {ex.Message}",
                    ActionName = "Index",
                    ControllerName = "Admin",
                    ErrorCode = 500
                };
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult EditUserRoles(UpdateUserRoleViewModel model)
    {
        //var userId = Request.Form[1].ToString();
        //var rolIdler = Request.Form[2].ToString().Split(',');
        var userId = model.Id;
        var rolIdler = model.Roles;
        var roleManager = NewRoleManager();
        var seciliRoller = new string[rolIdler.Count];
        for (var i = 0; i < rolIdler.Count; i++)
        {
            var rid = rolIdler[i];
            seciliRoller[i] = roleManager.FindById(rid).Name;
        }

        var userManager = NewUserManager();
        var user = userManager.FindById(userId);

        foreach (var identityUserRole in user.Roles.ToList())
        {
            userManager.RemoveFromRole(userId, roleManager.FindById(identityUserRole.RoleId).Name);
        }

        for (int i = 0; i < seciliRoller.Length; i++)
        {
            userManager.AddToRole(userId, seciliRoller[i]);
        }
            TempData["Message"] = "Güncelleme işlemi başarılı";
            ViewBag.ProjeList = GetProjectSelectList();

            return RedirectToAction("EditUser", new { id = userId });
    }
        
        [HttpGet]
        public ActionResult Create()
        {
            var roller = GetRoleList();
           
                foreach (var selectListItem in roller)
                {
                    
                        selectListItem.Selected = true;
                }
            

            ViewBag.RoleList = roller;
          

            

            return View();

        }
        public async Task<ActionResult> Create(RegisterLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View("Index", model);
            }
            try
            {
                var userManager = NewUserManager();
                var userStore = NewUserStore();

                var rm = model.RegisterViewModel;

                var user = await userManager.FindByNameAsync(rm.Username);
                if (user != null)
                {
                    ModelState.AddModelError("Username", "Bu mail adresi sistemde kayıtlı");
                    return View("Index", model);
                }
                var newUser = new User()
                {
                    UserName = rm.Username,
                    Name = rm.Name,
                    Surname = rm.Surname,
                    Email = rm.Email


                };
                var result = await userManager.CreateAsync(newUser, rm.Password);
                if (result.Succeeded)
                {
                    if (userStore.Users.Count() == 1)
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
                return RedirectToAction("Index");


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

        // GET: User/Delete/5

        [HttpGet]
        public ActionResult DeleteUser(string id)
        {
            var userManager = NewUserManager();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = userManager.FindById(id);

            var result = userManager.Delete(user);
            if (result.Succeeded)
            {


                return RedirectToAction("index");
            }
            return View("index");

        }
        // POST: User/Delete/5
        [HttpPost]
        public ActionResult DeleteUsera(string id)
        {
            var userManager = NewUserManager();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user =  userManager.FindById(id);

            var result =  userManager.Delete(user);
            if (result.Succeeded)
            {

           
            return RedirectToAction("index");
            }
            return View("index");

        }
    }
}
