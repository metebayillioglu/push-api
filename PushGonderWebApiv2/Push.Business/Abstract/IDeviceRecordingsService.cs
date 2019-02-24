using Push.Entity.Concreate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Business.Abstract
{
    public interface IDeviceRecordingsService
    {
        void SaveRegistrationId(string userId, string regId, int deviceType);
        int isUserHaveRegId(string userId, string regId, int deviceType);
        List<DeviceRecords> GetUsersDevice(string userId);
    }
}
