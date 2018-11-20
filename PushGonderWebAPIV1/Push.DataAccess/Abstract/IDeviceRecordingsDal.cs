
using Push.Entity.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.DataAccess.Abstract
{
    public interface IDeviceRecordingsDal : IMongoReprository<DeviceRecords>
    {
        void SaveRegistrationId(string kullaniciId, string regId, int cihazTuru);
        int isUserHaveRegId(string kullaniciId, string regId, int cihazTuru);
        List<DeviceRecords> GetUsersDevice(string userId);
    }
}
