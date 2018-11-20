using Push.Entity.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.DataAccess.Abstract
{
    public interface IUserKeysDal : IMongoReprository<UserKeys>
    {
        string AddUserKeyAndReturnKey(string userId);
        string GetUserKey(string userId);
        string GetUserIdFromKey(string key);
    }
}
