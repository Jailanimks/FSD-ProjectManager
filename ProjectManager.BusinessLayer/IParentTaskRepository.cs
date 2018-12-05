using System.Collections.Generic;
using ProjectManager.DataLayer;

namespace ProjectManager.BusinessLayer
{
    public interface IParentTaskRepository
    {
        /* Methods for ParentTask Maintenance */
        void AddParentTask(ParentTasks objTask);
        void EditParentTask(ParentTasks objTask);
        void RemoveParentTask(int TaskId);
        List<ParentTasks> GetAllParentTasks();
        ParentTasks GetParentTaskById(int TaskId);
    }
}
