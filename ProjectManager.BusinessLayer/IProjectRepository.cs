using System.Collections.Generic;
using ProjectManager.DataLayer;

namespace ProjectManager.BusinessLayer
{
    public interface IProjectRepository
    {
     
        /* Methods for Project Maintenance */
        void AddProject(Projects objProject);
        void EditProject(Projects objProject);
        void RemoveProject(int ProjectId);
        List<Projects> GetAllProjects();
        Projects GetProjectById(int ProjectId);

    }
}
