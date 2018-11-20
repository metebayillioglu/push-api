
using Push.Entity.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Push.DataAccess.Abstract;
namespace Push.DataAccess.Concreate
{
    public class MongoDeviceRecordingsTempDal : MongoEntityRepositoryBase<DeviceRecordsTemp>, IDeviceRecordingsTempDal
    {
        public void AddRecords(DeviceRecordsTemp model)
        {
            Add(model, "DevicesTemp");
        }
    }
}
