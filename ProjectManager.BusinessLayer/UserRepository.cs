using System.Collections.Generic;
using System.Linq;
using ProjectManager.DataLayer;

namespace ProjectManager.BusinessLayer
{
    public class UserRepository : IUserRepository
    {

        /* Methods for User Maintenance */

        public void AddUser(Users objUser)
        {
            using (var userContext = new DatabaseContext())
            {
                userContext.Entry(objUser).State = System.Data.Entity.EntityState.Added;
                userContext.User.Add(objUser);
                userContext.SaveChanges();
            }

        }

        public void EditUser(Users objUser)
        {
            Users objdata = new Users();
            using (var userContext = new DatabaseContext())
            {
                objdata = userContext.User.Find(objUser.UserID);
                if (objdata != null)
                {
                    objdata.FirstName = objUser.FirstName;
                    objdata.LastName = objUser.LastName;
                    objdata.EmployeeId = objUser.EmployeeId;
                }
                userContext.Entry(objdata).CurrentValues.SetValues(objUser);
                userContext.SaveChanges();
            }
        }

        public void RemoveUser(int UserId)
        {
            Users objdata = new Users();
            using (var userContext = new DatabaseContext())
            {
                objdata = userContext.User.Find(UserId);
                if (objdata != null)
                {
                    userContext.User.Remove(objdata);
                    userContext.SaveChanges();
                }

            }
        }


        public List<Users> GetAllUsers()
        {
            List<Users> userData = null;
            using (var userContext = new DatabaseContext())
            {
                userData = userContext.User.ToList();
            }
            return userData;
        }

        public Users GetUserById(int UserId)
        {
            Users objdata = new Users();
            objdata = null;
            using (var userContext = new DatabaseContext())
            {
                objdata = (from obUser in userContext.User where obUser.UserID == UserId select obUser).FirstOrDefault();

            }
            return objdata;

        }

    }
}
