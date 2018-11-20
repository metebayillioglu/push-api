
using Push.Entity.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Push.DataAccess.Abstract;
using Push.Core.Crypto;

namespace Push.DataAccess.Concreate
{
    public class MongoUserKeysDal : MongoEntityRepositoryBase<UserKeys>, IUserKeysDal
    {
        private ICryptyoService _cryptoManager;
        public MongoUserKeysDal(ICryptyoService cryptoManager)
        {
            _cryptoManager = cryptoManager;
        }
        public int FindMaxUserKey()
        {
            List<UserKeys> users = GetAllKeys();
            if (users.Count == 0)
                return 91;
            foreach (var x in users)
            {
                x.KeyString = Convert.ToInt32(_cryptoManager.Decrypt(_cryptoManager.Base64Decode(x.Key)));
            }
            users = users.OrderByDescending(t => t.KeyString).ToList();
            return users.First().KeyString + 1;

        }

        public string AddUserKeyAndReturnKey(string userId)
        {
            Guid id = Guid.NewGuid();
            var key = _cryptoManager.Base64Encode(_cryptoManager.Encrypt(FindMaxUserKey().ToString()));
        //    var key = _cryptoManager.Base64Encode(_cryptoManager.Encrypt(DateTime.Now.Ticks.ToString()));
            var keys = new UserKeys()
            {
                Key = key,
                UserId = userId,
                CreatedDate = DateTime.Now
            };
            Add(keys, "UserKeys");
            return key;

        }

        public List<UserKeys> GetAllKeys()
        {
            try
            {
                var users = GetAll("UserKeys");
                List<UserKeys> kullanicilar = new List<UserKeys>();
                if (users == null) return null;
                foreach (var x in users)
                {
                    kullanicilar.Add(x);
                }
                return kullanicilar;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public string GetUserIdFromKey(string key)
        {
            List<UserKeys> Student_List = new List<UserKeys>();
            var StuInfo = _database.GetCollection<UserKeys>("UserKeys");

            foreach (UserKeys Stu in StuInfo.FindAll())
            {
                if (Stu.Key == key)
                    return Stu.UserId;
            }

            return "";
        }

        public string GetUserKey(string userId)
        {
                List<UserKeys> Student_List = new List<UserKeys>();
                var StuInfo = _database.GetCollection<UserKeys>("UserKeys");

                foreach (UserKeys Stu in StuInfo.FindAll())
                {
                if (Stu.UserId == userId)
                    return Stu.Key;
                }

                return "";

                // return _database.FindUser<Kullanicilar>("Announcements").Find(res);
            
        }
    }
}
