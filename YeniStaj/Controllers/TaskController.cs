using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YeniStaj.Models.Context;
using YeniStaj.Models.Entities;
using YeniStaj.Models.ViewModels;

namespace YeniStaj.Controllers
{
    public class TaskController : BaseController
    {
        MyContext db = new MyContext();
        // GET: Task
        public ActionResult Index()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var sorgu = db.Tasks.ToList();
            return View(sorgu);
        }

        // GET: Task/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Task/Create
        public ActionResult Create()
        {
            ViewBag.ProjeList = GetProjectSelectList();
            return View();
        }

        // POST: Task/Create
        [HttpPost]
        public ActionResult Create(Task task)
        {
            if (ModelState.IsValid)
            {
                task.TaskOlusturmaTarihi = System.DateTime.Now;
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(task);
        }

        // GET: Task/Edit/5
        public ActionResult EditTask(int id)
        {
            ViewBag.ProjeList = GetProjectSelectList();
            var task = db.Tasks.Find(id);
            if (task==null)
            {
                return RedirectToAction("Index");

            }
            var model = new TaskViewModel()
            {
                Taskid = task.Taskid,
                TaskBaslik=task.TaskBaslik,
                TaskAciklama=task.TaskAciklama,
                TaskTeslimTarihi=task.TaskTeslimTarihi,
                Projeid = task.Projeid,
                project=task.project

            };

            return View(model);
        }

        // POST: Task/Edit/5
        [HttpPost]
        public ActionResult EditTask(TaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }
            try
            {
                var task = db.Tasks.Find(model.Taskid);
                task.Taskid = model.Taskid;
                task.TaskBaslik = model.TaskBaslik;
                task.TaskAciklama = model.TaskAciklama;
                task.TaskTeslimTarihi = model.TaskTeslimTarihi;
                task.Projeid = model.Projeid;
                task.project = model.project;
                db.SaveChanges();
                TempData["Message"] = "Güncelleme işlemi başarılı";
                return RedirectToAction("Index", "Task");
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

        // GET: Task/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Task/Delete/5
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
