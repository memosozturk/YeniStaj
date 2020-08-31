using YeniStaj.Models.Entities;

namespace YeniStaj.Identity
{
    public class ProjectManager<T>
    {
        private ProjectStore<Project> projectStore;

        public ProjectManager(ProjectStore<Project> projectStore)
        {
            this.projectStore = projectStore;
        }
    }
}