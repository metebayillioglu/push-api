using Push.Business.Abstract;
using Push.Core.Crypto;
using Push.DataAccess.Abstract;
using Push.Entity.Concreate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Business.Concreate
{
   public class UsersManager : IUsersService
    {
        private IUsersDal _iUsersDal;
        private ICryptyoService _icryptyoService;
        private IMailService _iMailService;
        public UsersManager(IMailService iMailService,IUsersDal iUsersDal, ICryptyoService icryptyoService)
        {
            _iUsersDal = iUsersDal;
            _icryptyoService = icryptyoService;
            _iMailService = iMailService;
        }

        public string AddUser(Users user)
        {
            return _iUsersDal.AddUser(user);
        }

        public int FindUserIdForNewUser()
        {
            return _iUsersDal.FindUserIdForNewUser();
        }

        public List<Users> GetAllUsers()
        {
            return _iUsersDal.GetAllUsers();
        }

        public Users GetUserByEmail(string email)
        {
            return _iUsersDal.GetUserByEmail(email);
        }

        public Users GetUserByUserId(string userId)
        {
            return _iUsersDal.GetUserByUserId(userId);

        }

        public int SendPasswordRecovery(string email)
        {

            try
            {

                Users kul = GetUserByEmail(email);
                if (kul == null)
                {
                    return -3;
                }
                StringBuilder html = new StringBuilder();
                html.Append("Dear " + kul.NameSurname);
                var key = "KullaniciId=" + kul.NameSurname + "&Tarih=" + DateTime.Now.AddDays(1);
                key = _icryptyoService.Base64Encode(_icryptyoService.Encrypt(key));
                string url = "http://pushforever.online/email/" + key;
                html.Append("<a href='" + url + "'>Please click here for reset password</a>");
                _iMailService.SendEmail(html.ToString(), "Email Reset", email);
                return 1;
            }
            catch (Exception ex)
            {
                return -2;
            }
        }

        public int UpdatePassword(string userId, string password)
        {
            return _iUsersDal.UpdatePassword(userId, password);
        }

        public Users UserControl(string email, string password)
        {
            return _iUsersDal.UserControl(email, password);
        }
    }
}
