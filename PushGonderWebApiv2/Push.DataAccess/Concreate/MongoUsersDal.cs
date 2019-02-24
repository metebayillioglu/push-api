using MongoDB.Driver.Builders;
using Push.Entity.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Push.DataAccess.Abstract;
using Push.Core.Crypto;

namespace Push.DataAccess.Concreate
{
    public class MongoUsersDal : MongoEntityRepositoryBase<Users>, IUsersDal
    {
        private ICryptyoService _icryptyoService;
 
        public MongoUsersDal(ICryptyoService icryptyoService)
        {
            _icryptyoService = icryptyoService;
         
        }

        public int FindUserIdForNewUser()
        {
            List<Users> users = GetAllUsers();
            if (users.Count == 0)
                return 1;
            foreach(var x in users)
            {
                x.UserNo = Convert.ToInt32(_icryptyoService.Decrypt(_icryptyoService.Base64Decode(x.UserId)));
            }
          users=  users.OrderByDescending(t => t.UserNo).ToList();
            return users.First().UserNo + 1;

        }

        public List<Users> GetAllUsers()
        {
            try
            {
                var users = GetAll("Users");
                List<Users> kullanicilar = new List<Users>();
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

        public string AddUser(Users kullanici)
        {
            try
            {
               
                kullanici.UserId = _icryptyoService.Base64Encode(_icryptyoService.Encrypt(FindUserIdForNewUser().ToString()));

                Add(kullanici, "Users");
                return kullanici.UserId;
            }
            catch (Exception ex)
            {
                return "hata";
            }
        }
       

        public Users UserControl(string email, string parola)
        {
            var res = Query.And(
                    Query<Users>.EQ(e => e.Email, email.ToLower()),
                    Query<Users>.EQ(e => e.Password, parola)
                );

            return _database.GetCollection<Users>("Users").FindOne(res);
        }

      

        public Users GetUserByEmail(string email)
        {
            List<Users> users = GetAllUsers();
            if (users.Count == 0)
                return null;
            foreach (var x in users)
            {
               if( x.Email == email)
                {
                    return x;
                }
            }
            return null;
        }

        public int UpdatePassword(string kullaniciId, string parola)
        {
            try
            {

                var res = Query<Users>.EQ(p => p.UserId, kullaniciId);
                var sorgu = _database.GetCollection<Users>("Users").FindOne(res);
                sorgu.Password = parola;

                var operation = Update<Users>.Replace(sorgu);

                _database.GetCollection<Users>("Users").Update(res, operation);



               /* Kullanicilar kul = KullaniciIddenKullaniciGetir(kullaniciId);
                kul.Parola = parola;

              var operation =  Update<Kullanicilar>.Replace(kul);
                _database.GetCollection<Kullanicilar>("Users").Update( operation);*/
                return 1;
            }
            catch (Exception ex)
            {
                return -2;
            }
        }

        public Users GetUserByUserId(string userId)
        {
            List<Users> users = GetAllUsers();
            if (users.Count == 0)
                return null;
            foreach (var x in users)
            {
                if (x.UserId == userId)
                {
                    return x;
                }
            }
            return null;
        }
    }
}
