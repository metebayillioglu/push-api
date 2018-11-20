using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Push.Business.Abstract;
using Push.Entity.Concreate;
using Push.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushGonderWebAPIV1.Controllers
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

        public void SendPush(AnnouncementsPushModel model)
        {
            _iSendPushService.SendPushNotification(model.user, model.title, model.message);
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
                    result.Css = "alert alert-success";
                    result.Text = "Your message succesfully send";
                    result.Status = "ok";
                    SendPush(model);
                    return Ok(result);
                }
                else
                {
                    result.Css = "alert alert-danger";
                    result.Text = "Error occured. Please try again";
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
