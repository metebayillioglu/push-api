using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Business.Abstract
{
    public interface IUserKeysService
    {
        string AddUserKeyAndReturnKey(string userId);
        string GetUserKey(string userId);
        string GetUserIdFromKey(string key);
    }
}
