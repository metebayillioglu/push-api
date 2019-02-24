using Push.Business.Abstract;
using Push.DataAccess.Abstract;
using Push.Entity.Concreate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Business.Concreate
{
    public class DeviceRecordingsTempmanager : IDeviceRecordingTempService
    {
        private IDeviceRecordingsTempDal _iDal;
        public DeviceRecordingsTempmanager(IDeviceRecordingsTempDal iDal)
        {
            _iDal = iDal;
        }
        public void Addtemp(DeviceRecordsTemp model)
        {
            _iDal.AddRecords(model);
        }
    }
}
