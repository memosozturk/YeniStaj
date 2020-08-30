using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YeniStaj.Models.Context;
using YeniStaj.Models.Entities;

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

        // GET: Project/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Project/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Project/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
