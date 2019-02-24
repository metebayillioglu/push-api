using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Push.Business.Abstract;
using Push.Entity.Concreate;
using Push.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushGonderWebAPIV3.Controllers
{
    [Route("api/v1/push")]

    public class PushController:Controller
    {
        private IAnnouncementService _iAnnouncementsService;
        private IUserKeysService _iUserKeysDal;
        private ISendPushService _iSendPushService;
        public PushController(IUserKeysService iUserKeysDal, ISendPushService iSendPushService, IAnnouncementService iAnnouncementsService)
        {
            _iUserKeysDal = iUserKeysDal;
            _iSendPushService = iSendPushService;
            _iAnnouncementsService = iAnnouncementsService;
        }

        public int SendPush(AnnouncementsPushModel model)
        {
           return _iSendPushService.SendPushNotification(model.user, model.title, model.message);
        }
        [HttpPost]
        public IActionResult Post(AnnouncementsPushModel model)
        {
            try
            {
                var userKey = _iUserKeysDal.GetUserKey(model.user);
                if (userKey != model.token)
                {
                    return StatusCode(401);
                }
                var duyuru = new Announcements()
                {
                    Title = model.title,
                    Message = model.message,
                    UserId = model.user,
                    AnnouncementId = "1",
                    AnnouncementNo = 1,
                    isDelete = 0,

                };
                int sonuc = _iAnnouncementsService.AddAnnouncements(duyuru);
                var result = new ReturnGeneralResultModel();
                if (sonuc > 0)
                {
                    int resultPush = SendPush(model);
                    if(resultPush == -2)
                    {
                        result.Text = "No device found";
                        result.Status = "false";
                        result.Css = "alert alert-danger";
                    }
                    else if(resultPush == -3)
                    {
                        result.Text = "Error Occured";
                        result.Status = "false";
                        result.Css = "alert alert-danger";
                    } else
                    {
                        result.Text = "Your message succesfully quesed and sen as soon as possible";
                        result.Status = "ok";
                        result.Css = "alert alert-success";
                    }



                    return Ok(result);
                }
                else
                {
                    result.Css = "alert alert-danger";
                    result.Text = "Annoncement not saved.Error occured. Please try again";
                    result.Status = "false";
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                var result = new ReturnGeneralResultModel();
                result.Css = "alert alert-danger";
                result.Text = ex.ToString();
                result.Status = "false";
                return Ok(result);
            }
        }
    }
}
