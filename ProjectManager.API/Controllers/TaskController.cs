using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity.Infrastructure;
using System.Web.Http.Description;
using ProjectManager.DataLayer;
using ProjectManager.BusinessLayer;
using System.Web.Http.Cors;
using ProjectManager.API.Models;
using System.Linq;

namespace ProjectManager.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TaskController : ApiController
    {

        ITaskRepository objTaskRepo = new TaskRepository();


        [HttpGet]
        [Route("api/Task/GetTasks")]
        public List<TaskData> GetTasks()
        {
            List<TaskData> taskDetail = null;  
            try
            {
                taskDetail = objTaskRepo.GetAllTasks();
            }
            catch (ApplicationException ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = ex.Message });
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadGateway, ReasonPhrase = ex.Message });
            }

            return taskDetail;
        }


        [HttpGet]
        [Route("api/Task/GetTaskById/{id}")]
        [ResponseType(typeof(TaskData))]
        public IHttpActionResult GetTaskById(int id)
        {
            IHttpActionResult retResult = null;
            TaskData taskData = new TaskData();
            try
            {
                taskData = objTaskRepo.GetTaskById(id);
                if (taskData == null)
                {
                    retResult =  NotFound();
                }

                retResult= Ok(taskData);

            }
            catch (ApplicationException ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = ex.Message });
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadGateway, ReasonPhrase = ex.Message });
            }

            return retResult;
        }


        [HttpGet]
        [Route("api/Task/GetProjectTasks")]
        [ResponseType(typeof(IEnumerable<ProjectTasks>))]
        public IHttpActionResult GetProjectTasks()
        {
            IHttpActionResult retResult = null;
            TaskData taskData = new TaskData();
          
            try
            {
         
                var prjTask = from p in objTaskRepo.GetAllTasks()
                        group p by p.ProjectID into g
                        select new ProjectTasks
                        {
                            ProjectID = g.Key,
                            TotalTask = g.Count(),
                            CompTask = g.Count(p => p.Status == true)
                        };

                 retResult = Ok(prjTask);
            }
            catch (ApplicationException ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = ex.Message });
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadGateway, ReasonPhrase = ex.Message });
            }

            return retResult;
        }

        

        [HttpPut]
        [Route("api/Task/EditTask/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult EditTask(int id, TaskData objtask)
        {
            
            TaskData taskData = new TaskData();
            if (!ModelState.IsValid)
            {
                return  BadRequest(ModelState);
            }

            if (id != objtask.TaskId)
            {
                return BadRequest();
            }
            
            try
            {
                this.objTaskRepo.EditTask(objtask);
            }
            catch (DbUpdateConcurrencyException)
            {
                taskData = objTaskRepo.GetTaskById(id);
                if (taskData == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }



        [HttpPost]
        [Route("api/Task/SaveTask")]
        [ResponseType(typeof(TaskData))]
        public IHttpActionResult SaveTask(TaskData objtask)
        {
            TaskData taskData = new TaskData();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
         
            try
            {
                this.objTaskRepo.AddTask(objtask);
            }
            catch (DbUpdateException)
            {
                taskData = objTaskRepo.GetTaskById(objtask.TaskId);

                if (taskData == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(objtask.TaskId);
            //return CreatedAtRoute("DefaultApi", new { id = objtask.TaskId }, objtask);
        }


        [HttpDelete]
        [Route("api/Task/DeleteTask/{id}")]
        [ResponseType(typeof(TaskData))]
        public IHttpActionResult DeleteTask(int id)
        {
            TaskData taskData = new TaskData();
            taskData = objTaskRepo.GetTaskById(id);
            if (taskData == null)
            {
                return NotFound();
            }
            this.objTaskRepo.RemoveTask(id);
            return Ok(taskData);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                objTaskRepo = null;
            }
            base.Dispose(disposing);
        }
        
    }
}
