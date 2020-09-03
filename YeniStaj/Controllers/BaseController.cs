using Microsoft.Build.Evaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YeniStaj.Identity;
using YeniStaj.Models.Context;
using YeniStaj.Models.Entities;
using YeniStaj.Models.ViewModels;
using YeniStaj.Repository;

namespace YeniStaj.Controllers
{
    
    [Authorize]
    public class BaseController : Controller
    {
        MyContext db = new MyContext();
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
        protected List<SelectListItem> GetTaskStateSelectList()
        {
            List<SelectListItem> degerler = (from x in db.TaskStates.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.TaskDurumu,
                                                 Value = x.TaskStateId.ToString()
                                             }).ToList();

            return degerler;
        }
       
        protected List<SelectListItem> GetProjectSelectList()
        {
            var projects = new ProjectRepo()
                .GetAll()
                ;
            var list = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "Üst Projesi yok",
                    Value = "XX"
                }
            };
            foreach (var project in projects)
            {
                if (project.ProjeAdi.Any())
                {
                    list.Add(new SelectListItem()
                    {
                        Text = project.ProjeAdi,
                        Value = project.Id.ToString()
                    });
                    
                }
                else
                {
                    list.Add(new SelectListItem()
                    {
                        Text =project.ProjeAdi,
                        Value = project.Id.ToString()
                    });

                    
                }
               
            }
                return list;


        }


            }
}