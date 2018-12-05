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

namespace ProjectManager.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ParentTaskController : ApiController
    {

        IParentTaskRepository objPtaskRepo = new ParentTaskRepository();

        [HttpGet]
        [Route("api/ParentTask/GetParentTasks")]
        public List<ParentTasks> GetParentTasks()
        {
            List<ParentTasks> ptaskDetail = null;
            try
            {
                ptaskDetail = objPtaskRepo.GetAllParentTasks();
            }
            catch (ApplicationException ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = ex.Message });
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadGateway, ReasonPhrase = ex.Message });
            }

            return ptaskDetail;
        }



        [HttpGet]
        [Route("api/ParentTask/GetParentTaskById/{id}")]
        [ResponseType(typeof(ParentTasks))]
        public IHttpActionResult GetParentTaskById(int id)
        {
            IHttpActionResult retResult = null;
            ParentTasks ptaskData = new ParentTasks();
            try
            {
                ptaskData = objPtaskRepo.GetParentTaskById(id);
                if (ptaskData == null)
                {
                    retResult = NotFound();
                }

                retResult = Ok(ptaskData);

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
        [Route("api/ParentTask/EditParentTask/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult EditParentTask(int id, ParentTasks objPTask)
        {

            ParentTasks ptaskData = new ParentTasks();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objPTask.ParentTaskId)
            {
                return BadRequest();
            }

            try
            {
                objPtaskRepo.EditParentTask(objPTask);
            }
            catch (DbUpdateConcurrencyException)
            {
                ptaskData = objPtaskRepo.GetParentTaskById(id);
                if (ptaskData == null)
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
        [Route("api/ParentTask/SaveParentTask")]
        [ResponseType(typeof(ParentTasks))]
        public IHttpActionResult SaveParentTask(ParentTasks objPTask)
        {
            ParentTasks ptaskData = new ParentTasks();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                objPtaskRepo.AddParentTask(objPTask);
            }
            catch (DbUpdateException)
            {
                ptaskData = objPtaskRepo.GetParentTaskById(objPTask.ParentTaskId);

                if (ptaskData == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(objPTask.ParentTaskId);
        }


        [HttpDelete]
        [Route("api/ParentTask/DeleteParentTask/{id}")]
        [ResponseType(typeof(ParentTasks))]
        public IHttpActionResult DeleteParentTask(int id)
        {
            ParentTasks ptaskData = new ParentTasks();
            ptaskData = objPtaskRepo.GetParentTaskById(id);
            if (ptaskData == null)
            {
                return NotFound();
            }
            objPtaskRepo.RemoveParentTask(id);
            return Ok(ptaskData);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                objPtaskRepo = null;
            }
            base.Dispose(disposing);
        }



    }
}
