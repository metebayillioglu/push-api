using Push.Business.Abstract;
using Push.DataAccess.Abstract;
using Push.Entity.Concreate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Business.Concreate
{
    public class DeviceRecordingsManager : IDeviceRecordingsService
    {
        private IDeviceRecordingsDal _iDeviceRecordingsDal; 
        public DeviceRecordingsManager(IDeviceRecordingsDal iDeviceRecordingsDal)
        {
            _iDeviceRecordingsDal = iDeviceRecordingsDal;
        }
        public List<DeviceRecords> GetUsersDevice(string userId)
        {
            return _iDeviceRecordingsDal.GetUsersDevice(userId);
        }

        public void SaveRegistrationId(string userId, string regId, int deviceType)
        {
            _iDeviceRecordingsDal.SaveRegistrationId(userId, regId, deviceType);
        }

        public int isUserHaveRegId(string userId, string regId, int deviceType)
        {
            return _iDeviceRecordingsDal.isUserHaveRegId(userId, regId, deviceType);
        }
    }
}
