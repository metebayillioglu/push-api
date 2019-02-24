
using Push.Entity.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.DataAccess.Abstract
{
    public interface IDeviceRecordingsTempDal : IMongoReprository<DeviceRecordsTemp>
    {
      void  AddRecords(DeviceRecordsTemp model);
    }
}
