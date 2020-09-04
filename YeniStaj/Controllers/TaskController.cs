using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YeniStaj.Models.Context;
using YeniStaj.Models.Entities;
using YeniStaj.Models.IdentityModels;
using YeniStaj.Models.ViewModels;
using static YeniStaj.Identity.MembershipTools;

namespace YeniStaj.Controllers
{
    [Authorize]
    public class TaskController : BaseController
    {
        MyContext db = new MyContext();
        // GET: Task
        [Authorize(Roles="Admin,PoweredUser")]
        public ActionResult Index()
        {
            
            db.Configuration.LazyLoadingEnabled = false;
            var sorgu = db.Tasks.ToList();
            return View(sorgu);
        }
        public ActionResult Export()
        {
            var sorgu = db.Tasks.ToList();
            return View(sorgu);
        }

        // GET: Task/Details/5
        [Authorize(Roles = "Admin,PoweredUser")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Task/Create
        [Authorize(Roles = "Admin,PoweredUser")]
        public ActionResult Create()
        {
            ViewBag.TaskStateList = GetTaskStateSelectList();
            ViewBag.ProjeList = GetProjectSelectList();
            return View();
        }

        // POST: Task/Create
        [HttpPost]
        [Authorize(Roles = "Admin,PoweredUser")]
        public ActionResult Create(Task task)
        {
            if (ModelState.IsValid)
            {
                task.TaskOlusturmaTarihi = System.DateTime.Now;
                task.TaskStateId = 1;
                task.TaskDurumu = "Waiting";
                
                
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(task);
        }

        // GET: Task/Edit/5
        [Authorize(Roles = "Admin,PoweredUser")]
        public ActionResult EditTask(int id)
        {
            ViewBag.TaskStateList = GetTaskStateSelectList();
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
                TaskStateId=task.TaskStateId,
               
                
                Projeid = task.Projeid,
                project=task.project

            };

            return View(model);
        }

        // POST: Task/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin,PoweredUser")]
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
                task.TaskStateId = model.TaskStateId;
                task.TaskDurumu = task.TaskDurumu;
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
        [Authorize(Roles = "Admin,PoweredUser")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Task/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin,PoweredUser")]
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
        [HttpGet]
        [Authorize(Roles = "User")]
        public ActionResult ProjeTask()
        {

            var id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
            var user = NewUserManager().FindById(id);

            
            var abc = db.Tasks.Where(x => x.Projeid ==user.Projeid).ToList();
            return View(abc);
        }
        [HttpPost]
        public ActionResult ProjeTask(TaskViewModel model)
        {
            return View();
        }
        [HttpGet]
        public ActionResult WaitingTask()
        {
            var id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
            var user = NewUserManager().FindById(id);

            var listele = db.Tasks.Where(x => x.Projeid ==user.Projeid && x.TaskStateId==1).ToList();
            return View(listele);

        }
        [HttpPost]
        public ActionResult WaitingTask(TaskViewModel model)
        {
            return View();
        }
        [HttpGet]
        public ActionResult WorkingTask()
        {
            var id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
            var user = NewUserManager().FindById(id);

            var listele = db.Tasks.Where(x => x.Projeid == user.Projeid && x.TaskStateId == 2).ToList();
            return View(listele);

        }
        [HttpPost]
        public ActionResult WorkingTask(TaskViewModel model)
        {
            
            return View();
        }

        [HttpGet]
        public ActionResult FinishedTask()
        {
            var id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
            var user = NewUserManager().FindById(id);

            var listele = db.Tasks.Where(x => x.Projeid == user.Projeid && x.TaskStateId == 3).ToList();
            return View(listele);

        }
        [HttpPost]
        public ActionResult FinishedTask(TaskViewModel model)
        {
            return View();
        }
        [HttpGet]
        public ActionResult TakeTask(int id)
        {
            if (ModelState.IsValid)
            {

            
            var taskbul = db.Tasks.Where(x=>x.Taskid==id).SingleOrDefault();

                var userid = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
                var user = NewUserManager().FindById(userid);
                taskbul.Username = user.UserName;
                taskbul.TaskStateId = 2;
               
                db.SaveChanges();
                return RedirectToAction("ProjeTask");
            }


            return View("ProjeTask");
        }
        [HttpGet]
        public ActionResult TaskDeliver(int id)
        {
            
                var taskbul = db.Tasks.Where(x=>x.Taskid==id).SingleOrDefault();
                var userid = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
                var user = NewUserManager().FindById(userid);
                if (taskbul.Username!=user.UserName)
                {
                    return RedirectToAction("ProjeTask", "Task");

                }
                var model = new TaskViewModel()
                {
                    Taskid = taskbul.Taskid,
                    TaskBaslik = taskbul.TaskBaslik,
                    TaskAciklama = taskbul.TaskAciklama,
                    TaskTeslimTarihi = taskbul.TaskTeslimTarihi,
                    TaskStateId = taskbul.TaskStateId,
                    Projeid = taskbul.Projeid,
                    project = taskbul.project,
                    Username = taskbul.Username,
                    GeriBildirim=taskbul.GeriBildirim,
                    GeriBildirimTarihi=taskbul.GeribildirimTarihi

                };

            
            return View(model);
        }
        [HttpPost]
        public ActionResult TaskDeliver(int id,TaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }
            try
            {
              
                var task = db.Tasks.Where(x=>x.Taskid==id).SingleOrDefault();
                task.TaskStateId = 3;
                task.GeriBildirim = model.GeriBildirim;
                task.GeribildirimTarihi = System.DateTime.Now;
                db.SaveChanges();
                TempData["Message"] = "Güncelleme işlemi başarılı";
                return RedirectToAction("WorkingTask", "Task");
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
       
    }
}
