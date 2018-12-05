using System.Collections.Generic;
using System.Linq;
using ProjectManager.DataLayer;

namespace ProjectManager.BusinessLayer
{
    public class ParentTaskRepository : IParentTaskRepository
    {


        /* Methods for ParentTask Maintenance */
        public void AddParentTask(ParentTasks objTask)
        {
            using (var ptaskContext = new DatabaseContext())
            {
                ptaskContext.Entry(objTask).State = System.Data.Entity.EntityState.Added;
                ptaskContext.ParentTask.Add(objTask);
                ptaskContext.SaveChanges();
            }
        }

        public void EditParentTask(ParentTasks objTask)
        {
            ParentTasks objdata = new ParentTasks();
            using (var ptaskContext = new DatabaseContext())
            {
                objdata = ptaskContext.ParentTask.Find(objTask.ParentTaskId);
                if (objdata != null)
                {
                    objdata.ParentTaskName = objTask.ParentTaskName;
                    objdata.ProjectID = objTask.ProjectID;
                }
                ptaskContext.Entry(objdata).CurrentValues.SetValues(objTask);
                ptaskContext.SaveChanges();
            }

        }


        public void RemoveParentTask(int TaskId)
        {
            ParentTasks objdata = new ParentTasks();
            using (var ptaskContext = new DatabaseContext())
            {
                objdata = ptaskContext.ParentTask.Find(TaskId);
                if (objdata != null)
                {
                    ptaskContext.ParentTask.Remove(objdata);
                    ptaskContext.SaveChanges();
                }

            }
        }



        public List<ParentTasks> GetAllParentTasks()
        {
            List<ParentTasks> ptaskData = null;
            using (var ptaskContext = new DatabaseContext())
            {
                ptaskData = ptaskContext.ParentTask.ToList();
            }
            return ptaskData;

        }

        public ParentTasks GetParentTaskById(int TaskId)
        {
            ParentTasks objdata = new ParentTasks();
            objdata = null;
            using (var ptaskContext = new DatabaseContext())
            {
                objdata = (from obTask in ptaskContext.ParentTask where obTask.ParentTaskId == TaskId select obTask).FirstOrDefault();

            }
            return objdata;
        }



    }
}
