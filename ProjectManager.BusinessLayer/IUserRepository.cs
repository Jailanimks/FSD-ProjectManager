using System.Collections.Generic;
using ProjectManager.DataLayer;

namespace ProjectManager.BusinessLayer
{
    public interface IUserRepository
    {

        /* Methods for User Maintenance */
        void AddUser(Users objUser);
        void EditUser(Users objUser);
        void RemoveUser(int UserId);
        List<Users> GetAllUsers();
        Users GetUserById(int UserId);

    }
}
