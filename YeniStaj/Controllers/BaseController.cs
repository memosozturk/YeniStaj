using Microsoft.Build.Evaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YeniStaj.Identity;
using YeniStaj.Models.Entities;
using YeniStaj.Models.ViewModels;
using YeniStaj.Repository;

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


        protected List<SelectListItem> GetProjectSelectList()
        {
            var projects = new ProjectRepo()
                .GetAll(x => x.Id == null)
                .OrderBy(x => x.ProjeAdi);
            var list = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "Üst Projesi yok",
                    Value = "0"
                }
            };
            foreach (var Project in projects)
            {
                if (Project.ProjeAdi.Any())
                {
                    list.Add(new SelectListItem()
                    {
                        Text = Project.ProjeAdi,
                        Value = Project.Id.ToString()
                    });
                    
                }
                else
                {
                    list.Add(new SelectListItem()
                    {
                        Text =Project.ProjeAdi,
                        Value = Project.Id.ToString()
                    });

                    
                }
            }
                return list;


        }


            }
}