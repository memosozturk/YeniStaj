using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YeniStaj.Models.Context;
using YeniStaj.Models.Entities;
using YeniStaj.Models.ViewModels;

namespace YeniStaj.Controllers
{
    public class ProjectController : BaseController
    {
        MyContext db = new MyContext();
        // GET: Project
        public ActionResult Index()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var sorgu = db.Projects.ToList();
            return View(sorgu);
        }

        
        

        // GET: Project/Create
        public ActionResult Create()
        {
            var kullanıcılar = GetUserList();

            foreach (var selectListItem in kullanıcılar)
            {

                selectListItem.Selected = true;
            }


            ViewBag.Userlist = kullanıcılar;
            ViewBag.ProjeList = GetProjectSelectList();
            return View();
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                project.EklenmeTarihi = System.DateTime.Now;
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(project);
        }

        // GET: Project/Edit/5
        public ActionResult EditProject(int id)
        {
            var project = db.Projects.Find(id);
            if (project==null)
            {
                return RedirectToAction("Index");
            }
            var model = new ProjectIndexViewModel() {
                Id=project.Id,
                ProjeAdi = project.ProjeAdi,
                ProjeAciklama = project.ProjeAciklama,
                EklenmeTarihi=project.EklenmeTarihi
                    
            };
            return View(model);
        }

        // POST: Project/Edit/5
        [HttpPost]
        public ActionResult EditProject(ProjectIndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }
            try
            {
                var project = db.Projects.Find(model.Id);
                project.ProjeAdi = model.ProjeAdi;
                project.ProjeAciklama = model.ProjeAciklama;
                db.SaveChanges();
                TempData["Message"] = "Güncelleme işlemi başarılı";
                return RedirectToAction("Index","Project");
                // TODO: Add update logic here

               
            }
            catch(Exception ex)
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

       
        // POST: Project/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();

            }
            return View(project);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
