using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Drawing;
using System.Web;
using YeniStaj.Models.Context;
using YeniStaj.Models.Entities;
using YeniStaj.Models.IdentityModels;


namespace YeniStaj.Identity
{
    public static class MembershipTools
    {
       public static MyContext db = new MyContext();
        private static MyContext _db;
        public static UserStore<User> NewUserStore() => new UserStore<User>(_db ?? new MyContext());
        public static UserManager<User> NewUserManager() => new UserManager<User>(NewUserStore());
        public static RoleStore<Role> NewRoleStore() => new RoleStore<Role>(_db ?? new MyContext());
        public static RoleManager<Role> NewRoleManager() => new RoleManager<Role>(NewRoleStore());
        public static ProjectStore<Project> NewProjectStore() => new ProjectStore<Project>(_db ?? new MyContext());
        public static ProjectManager<Project> NewProjectManager() => new ProjectManager<Project>(NewProjectStore());
        public static string GetNameSurname(string userId)
        {
            User user;
            if (string.IsNullOrEmpty(userId))
            {
              var id=HttpContext.Current.User.Identity.GetUserId();
                if (string.IsNullOrEmpty(id))
                    return "";

                user = NewUserManager().FindById(id);
             }
            else
            {
                user = NewUserManager().FindById(userId);
                if (user==null)
                {
                    return null;

                }

            }
            return $"{user.Name} {user.Surname}";
        }
        public static String GetTaskStateName(int id)
        {
            Models.Entities.TaskState taskState;
            taskState = db.TaskStates.Find(id);
            string stateadi = taskState.TaskDurumu;
            if (String.IsNullOrEmpty(stateadi))
            {
                return "null";
            }
            return stateadi;
        }
        public static String GetProjectName(int id)
        {
            Models.Entities.Project project;
            project = db.Projects.Find(id);
            string projeadii = project.ProjeAdi;
            return projeadii;
        }

    }

    public class ProjectStore<T>
    {
        private MyContext myContext;

        public ProjectStore(MyContext myContext)
        {
            this.myContext = myContext;
        }
    }
}