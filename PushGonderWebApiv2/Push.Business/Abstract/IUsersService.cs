using Push.Entity.Concreate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Business.Abstract
{
   public interface IUsersService
    {
        string AddUser(Users user);
        List<Users> GetAllUsers();
        int FindUserIdForNewUser();
        Users UserControl(string email, string password);
        Users GetUserByEmail(string email);
        int SendPasswordRecovery(string email);
        int UpdatePassword(string userId, string password);
        Users GetUserByUserId(string userId);
    }
}
