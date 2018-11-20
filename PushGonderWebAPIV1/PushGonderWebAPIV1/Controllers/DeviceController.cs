using Microsoft.AspNetCore.Mvc;
using Push.Business.Abstract;
using Push.Entity.Concreate;
using Push.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushGonderWebAPIV1.Controllers
{
    [Route("api/devices")]
    public class DeviceController:Controller
    {
        private IDeviceRecordingsService _iDeviceRecordingsService;
        private IDeviceRecordingTempService _iDeviceRecordingsTempService;
        public DeviceController(IDeviceRecordingsService iDeviceRecordingsService, IDeviceRecordingTempService iDeviceRecordingsTempService)
        {
            _iDeviceRecordingsTempService = iDeviceRecordingsTempService;
            _iDeviceRecordingsService = iDeviceRecordingsService;
        }
        [HttpPost]
        public IActionResult Post(RegistraionFromModel model)
        {
            string userId = "";
            string regId = "";
            DeviceRecordsTemp mode = new DeviceRecordsTemp();
            try
            {
                 userId = model.UserId;
                 regId = model.RegId;
                mode.RegId = model.RegId;
                mode.UserId = model.UserId;
                _iDeviceRecordingsTempService.Addtemp(mode);
            }
            catch (Exception ex)
            {


                mode.RegId = ex.ToString();
                mode.UserId = ex.ToString();
                _iDeviceRecordingsTempService.Addtemp(mode);
            }
            int deviceType = 0;
            if (regId.Contains(":"))
            {
                deviceType = 1;
            }
            ReturnGeneralResultModel result = new ReturnGeneralResultModel();
            try
            {
                _iDeviceRecordingsService.SaveRegistrationId(userId, regId, deviceType);
                result.Status = "ok";
                result.Text = "ok";
            }
            catch (Exception ex)
            {

                mode.RegId ="2"+ ex.ToString();
                mode.UserId = "2" +ex.ToString();
                _iDeviceRecordingsTempService.Addtemp(mode);
                result.Status = "hata";
                result.Text = ex.ToString();
            }

        
            return Ok(result);
        }
    }
}
