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
    public class ProjectController : ApiController
    {

        IProjectRepository objProjectRepo = new ProjectRepository();

        [HttpGet]
        [Route("api/Project/GetProjects")]
        public List<Projects> GetProjects()
        {
            List<Projects> projectDetail = null;
            try
            {
                projectDetail = objProjectRepo.GetAllProjects();
            }
            catch (ApplicationException ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = ex.Message });
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadGateway, ReasonPhrase = ex.Message });
            }

            return projectDetail;
        }



        [HttpGet]
        [Route("api/Project/GetProjectById/{id}")]
        [ResponseType(typeof(Projects))]
        public IHttpActionResult GetProjectById(int id)
        {
            IHttpActionResult retResult = null;
            Projects projectData = new Projects();
            try
            {
                projectData = objProjectRepo.GetProjectById(id);
                if (projectData == null)
                {
                    retResult = NotFound();
                }

                retResult = Ok(projectData);

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
        [Route("api/Project/EditProject/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult EditProject(int id, Projects objProject)
        {

            Projects projectData = new Projects();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objProject.ProjectID)
            {
                return BadRequest();
            }

            try
            {
                objProjectRepo.EditProject(objProject);
            }
            catch (DbUpdateConcurrencyException)
            {
                projectData = objProjectRepo.GetProjectById(id);
                if (projectData == null)
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
        [Route("api/Project/SaveProject")]
        [ResponseType(typeof(Projects))]
        public IHttpActionResult SaveProject(Projects objProject)
        {
            Projects projectData = new Projects();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                objProjectRepo.AddProject(objProject);
            }
            catch (DbUpdateException)
            {
                projectData = objProjectRepo.GetProjectById(objProject.ProjectID);

                if (projectData == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(objProject.ProjectID);
        }


        [HttpDelete]
        [Route("api/Project/DeleteProject/{id}")]
        [ResponseType(typeof(Projects))]
        public IHttpActionResult DeleteProject(int id)
        {
            Projects projectData = new Projects();
            projectData = objProjectRepo.GetProjectById(id);
            if (projectData == null)
            {
                return NotFound();
            }
            objProjectRepo.RemoveProject(id);
            return Ok(projectData);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                objProjectRepo = null;
            }
            base.Dispose(disposing);
        }



    }
}
