
using Push.Entity.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.DataAccess.Abstract
{
    public interface IUsersDal : IMongoReprository<Users>
    {
        string AddUser(Users kullanici);
        List<Users> GetAllUsers();
        int FindUserIdForNewUser();
        Users UserControl(string email, string parola);
        Users GetUserByEmail(string email);
        int UpdatePassword(string kullaniciId,string parola);
        Users GetUserByUserId(string kullaniciId);

    }
}
