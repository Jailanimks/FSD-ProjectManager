using System.Collections.Generic;
using System.Linq;
using ProjectManager.DataLayer;


namespace ProjectManager.BusinessLayer
{
    public class ProjectRepository : IProjectRepository
    {

        /* Methods for Project Maintenance */
        public void AddProject(Projects objProject)
        {
            using (var projectContext = new DatabaseContext())
            {
                projectContext.Entry(objProject).State = System.Data.Entity.EntityState.Added;
                projectContext.Project.Add(objProject);
                projectContext.SaveChanges();
            }
        }

        public void EditProject(Projects objProject)
        {
            Projects objdata = new Projects();
            using (var projectContext = new DatabaseContext())
            {
                objdata = projectContext.Project.Find(objProject.ProjectID);
                if (objdata != null)
                {
                    objdata.ProjectName = objProject.ProjectName;
                    objdata.StartDate = objProject.StartDate;
                    objdata.EndDate = objProject.EndDate;
                    objdata.ManagerID = objProject.ManagerID;
                    objdata.Suspended = objProject.Suspended;
                }
                projectContext.Entry(objdata).CurrentValues.SetValues(objProject);
                projectContext.SaveChanges();
            }

        }


        public void RemoveProject(int ProjectId)
        {
            Projects objdata = new Projects();
            using (var projectContext = new DatabaseContext())
            {
                objdata = projectContext.Project.Find(ProjectId);
                if (objdata != null)
                {
                    projectContext.Project.Remove(objdata);
                    projectContext.SaveChanges();
                }

            }
        }

        public List<Projects> GetAllProjects()
        {
            List<Projects> projectData = null;
            using (var projectContext = new DatabaseContext())
            {
                projectData = projectContext.Project.ToList();
            }
            return projectData;
        }

        public Projects GetProjectById(int ProjectId)
        {
            Projects objdata = new Projects();
            objdata = null;
            using (var projectContext = new DatabaseContext())
            {
                objdata = (from obProject in projectContext.Project where obProject.ProjectID == ProjectId select obProject).FirstOrDefault();

            }
            return objdata;
        }




               
    }
}
