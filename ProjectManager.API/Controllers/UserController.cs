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
    public class UserController : ApiController
    {

        IUserRepository objUserRepo = new UserRepository();

        [HttpGet]
        [Route("api/User/GetUsers")]
        public List<Users> GetUsers()
        {
            List<Users> userDetail = null;
            try
            {
                userDetail = objUserRepo.GetAllUsers();
            }
            catch (ApplicationException ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = ex.Message });
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadGateway, ReasonPhrase = ex.Message });
            }

            return userDetail;
        }



        [HttpGet]
        [Route("api/User/GetUserById/{id}")]
        [ResponseType(typeof(Users))]
        public IHttpActionResult GetUserById(int id)
        {
            IHttpActionResult retResult = null;
            Users userData = new Users();
            try
            {
                userData = objUserRepo.GetUserById(id);
                if (userData == null)
                {
                    retResult = NotFound();
                }

                retResult = Ok(userData);

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
        [Route("api/User/EditUser/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult EditUser(int id, Users objUser)
        {

            Users userData = new Users();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objUser.UserID)
            {
                return BadRequest();
            }

            try
            {
                objUserRepo.EditUser(objUser);
            }
            catch (DbUpdateConcurrencyException)
            {
                userData = objUserRepo.GetUserById(id);
                if (userData == null)
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
        [Route("api/User/SaveUser")]
        [ResponseType(typeof(Users))]
        public IHttpActionResult SaveUser(Users objUser)
        {
            Users userData = new Users();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                objUserRepo.AddUser(objUser);
            }
            catch (DbUpdateException)
            {
                userData = objUserRepo.GetUserById(objUser.UserID);

                if (userData == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(objUser.UserID);
        }


        [HttpDelete]
        [Route("api/User/DeleteUser/{id}")]
        [ResponseType(typeof(Users))]
        public IHttpActionResult DeleteUser(int id)
        {
            Users userData = new Users();
            userData = objUserRepo.GetUserById(id);
            if (userData == null)
            {
                return NotFound();
            }
            objUserRepo.RemoveUser(id);
            return Ok(userData);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                objUserRepo = null;
            }
            base.Dispose(disposing);
        }



    }
}
