using System.Collections.Generic;
using ProjectManager.DataLayer;

namespace ProjectManager.BusinessLayer
{
    public interface ITaskRepository
    {

        /* Methods for Task Maintenance */

        void AddTask(TaskData objTask);
        void EditTask(TaskData objTask);
        void RemoveTask(int Id);
        List<TaskData> GetAllTasks();
        TaskData GetTaskById(int Id);

    }
}
