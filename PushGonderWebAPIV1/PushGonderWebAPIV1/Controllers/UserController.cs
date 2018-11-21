using Microsoft.AspNetCore.Mvc;
using Push.Business.Abstract;
using Push.Core.Crypto;
using Push.Entity.Concreate;
using Push.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushGonderWebAPIV1.Controllers
{
    [Route("api/users")]
    public class UserController:Controller
    {
        private IUsersService _iUserService;
        private ICryptyoService _iCryptoService;
        private IUserKeysService _iuserKeysDal;
        private ILoginTempService _login;
        public UserController(ILoginTempService login,IUsersService iUserService, ICryptyoService iCryptoService, IUserKeysService iuserKeysDal)
        {
            _iUserService = iUserService;
            _iCryptoService = iCryptoService;
            _iuserKeysDal = iuserKeysDal;
            _login = login;
        }
        [HttpPost]
        public IActionResult Post(RegisterModel model)
        {
            Users user = new Users()
            {
                Email = model.Email,
                Password = _iCryptoService.HashPassword( model.Password),
                NameSurname = model.NameSurname,
                isDelete = 0
            };

            string userId = _iUserService.AddUser(user);
          
            var result = new ReturnGeneralModel()
            {
                Key = _iuserKeysDal.AddUserKeyAndReturnKey(userId),
                Css = "",
                UserId = userId,
                Status = "",
                Text = "",
                NameSurname =model.NameSurname
            };
            return Ok(result);

        }

        [HttpPost("UserControl")]
        public IActionResult UserControl(LoginModel model)
        {
          
            try
            {
                LoginTemp test = new LoginTemp();
                test.email = model.Email.ToLower();
                test.password = model.Password;
                test.returnpassword = "";
                test.namesurname = "";
                _login.AddTemp(test);
            }
            catch(Exception ex)
            {
                LoginTemp test = new LoginTemp();
                test.email = ex.ToString();
                test.password = ex.ToString();
                test.returnpassword = "";
                test.namesurname = "";
                _login.AddTemp(test);

            }
            var hashPassword = _iCryptoService.HashPassword(model.Password);
          Users kul = _iUserService.UserControl(model.Email.ToLower(), hashPassword);

            LoginTemp test2 = new LoginTemp();
            test2.email = "gelen";
            test2.password = "gelen";
            test2.returnpassword = hashPassword;
            test2.namesurname = "";
          
            _login.AddTemp(test2);
            if (kul == null)
            {
                var resultError = new ReturnGeneralModel()
                {
                    Key = "",
                    Css = "",
                    UserId = "hata",
                    Status = "",
                    NameSurname = "",
                    Text = ""
                };
                return Ok(resultError);
            }
           

            var cryptoKullaniciId = kul.UserId;// _iCryptoService.Base64Encode(_iCryptoService.Encrypt(kul.KullaniciId.ToString()));
            var key = _iuserKeysDal.GetUserKey(cryptoKullaniciId);
        var donenSonuc = new ReturnGeneralModel()
        {
            Key= key,
            Css = "",
            UserId = cryptoKullaniciId,
            Status = "",
            Text = "",
            NameSurname = kul.NameSurname
        };
            return Ok(donenSonuc);

    }

        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordModel model)
        {
            var css = "";
            var status = "";
            var text = "";
            string cryptotext = _iCryptoService.Decrypt( _iCryptoService.Base64Decode(model.Id));
            string[] bol = cryptotext.Split('&');
            var donenSonuchata = new ReturnGeneralResultModel();
            if (DateTime.Now > Convert.ToDateTime(bol[1].Replace("Date=","")))
            {
                donenSonuchata.Css = "alert alert-danger";
                donenSonuchata.Status = "fail";
                donenSonuchata.Text = "Link is expired";
                return Ok(donenSonuchata);

            }


            int sonuc = _iUserService.UpdatePassword(bol[0].Replace("UserId=",""), _iCryptoService.HashPassword(model.Password));

         
            if (sonuc < 0)
            {
                css = "alert alert-danger";
                status = "error";
                text = "Error Occured.Please try again";
            }
            else
            {

                css = "alert alert-success";
                status = "ok";
                text = "Your Password successfully changed";
            }


            donenSonuchata.Css = css;
                donenSonuchata.Status = status;
                donenSonuchata.Text = text;
            
            return Ok(donenSonuchata);





        }
        
        [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
          int sonuc = _iUserService.SendPasswordRecovery(email);

            var css = "";
            var status = "";
            var text = "";
            if(sonuc == -3)
            {
                css = "alert alert-danger";
                status = "error";
                text = "No user Found";
            }else if (sonuc == -2)
            {

                css = "alert alert-danger";
                status = "error";
                text = "Error Occured.Please try again";
            }
            else
            {

                css = "alert alert-success";
                status = "ok";
                text = "Password reset link send your email address";
            }
                var donenSonuchata = new ReturnGeneralResultModel()
                {
                   Css=css,
                   Status= status,
                   Text =text,
                };
                return Ok(donenSonuchata);
            




        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    List<string> kul = new List<string>();
        //    DateTime time = DateTime.Now;
        //    for(int i = 0; i <= 1000; i++)
        //    {
        //        Guid id = Guid.NewGuid();
        //        kul.Add(_iCryptoService.Base64Encode(_iCryptoService.Encrypt(id.ToString())));
        //    }
        //    return Ok(kul);
        //}
    }
}
