using Push.Business.Abstract;
using Push.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Business.Concreate
{
    public class UserKeysManager : IUserKeysService
    {
        private IUserKeysDal _iUserKeysDal;
        public UserKeysManager(IUserKeysDal iUserKeysDal)
        {
            _iUserKeysDal = iUserKeysDal;
        }
        public string AddUserKeyAndReturnKey(string userId)
        {
            return _iUserKeysDal.AddUserKeyAndReturnKey(userId);
            
        }

        public string GetUserIdFromKey(string key)
        {

            return _iUserKeysDal.GetUserIdFromKey(key);
        }

        public string GetUserKey(string userId)
        {

            return _iUserKeysDal.GetUserKey(userId);
        }
    }
}
