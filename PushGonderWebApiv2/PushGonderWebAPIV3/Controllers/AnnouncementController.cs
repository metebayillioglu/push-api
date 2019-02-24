using Microsoft.AspNetCore.Mvc;
using Push.Business.Abstract;
using Push.Core.Crypto;
using Push.Entity.Concreate;
using Push.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushGonderWebAPIV3.Controllers
{
    [Route("api/announcements")]
    public class AnnouncementController : Controller
    {
        private IAnnouncementService _iAnnouncementsService;
        private ICryptyoService _iCryptoService;
        private IUserKeysService _iUserKeysDal;
        private ISendPushService _iSendPushService;
        public AnnouncementController(IAnnouncementService iAnnouncementsService, ICryptyoService iCryptoService, IUserKeysService iUserKeysDal, ISendPushService iSendPushService)
        {
            _iAnnouncementsService = iAnnouncementsService;
            _iCryptoService = iCryptoService;
            _iUserKeysDal = iUserKeysDal;
            _iSendPushService = iSendPushService;
        }
        [HttpPost]
        public IActionResult Post(AnnouncementsFromModel model)
        {
            var userKey = _iUserKeysDal.GetUserKey(model.UserId);
            if (userKey != model.Key)
            {
                return StatusCode(401);
            }
            var annoucement = new Announcements()
            {
                Title = model.AnnouncementTitle,
                Message = model.AnnouncementMesage,
                UserId = model.UserId,
                Counter = 0,
                Date = DateTime.Now,
                AnnouncementId = "1",
                AnnouncementNo = 1,
                isDelete = 0,

            };
            int announcementId = _iAnnouncementsService.AddAnnouncements(annoucement);
            _iSendPushService.SendPushNotification(model.UserId, model.AnnouncementMesage, model.AnnouncementMesage);
            var result = new ReturnGeneralResultModel();
            if (announcementId > 0)
            {
                result.Css = "alert alert-success";
                result.Text = "Your message succesfully send";
                result.Status = "ok";
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

        [HttpGet]
        public IActionResult Get(string userId,string key)
        {
            var userKey = _iUserKeysDal.GetUserKey(userId);
            if(userKey != key)
            {
                return StatusCode(401);
            }
            List<AnnouncementsReturnModel> announcements = _iAnnouncementsService.GetAnnouncenmentsFromUserId(userId);
            return new ObjectResult(announcements);
        }

        [HttpGet("DeleteAnnouncements")]
        public IActionResult DeleteAnnouncements(string annoouncementsId, string key)
        {
            string userId = _iUserKeysDal.GetUserIdFromKey(key);
            if(userId == "")
            {
                return StatusCode(401);
            }
            int resultDelete = _iAnnouncementsService.DeleteAnnouncements(annoouncementsId,userId);
            var result = new ReturnGeneralResultModel();
            if (resultDelete > 0)
            {
                result.Css = "alert alert-success";
                result.Text = "Selected message succesfully deleted";
                result.Status = "ok";
                return Ok(result);
            }
            else
            {
                result.Css = "alert alert-danger";
                result.Text = "Error occured. please try again";
                result.Status = "false";
                return Ok(result);
            }
        }
    }
}
