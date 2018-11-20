
using Push.DataAccess.Abstract;
using Push.Entity.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.DataAccess.Concreate
{
    public class MongoDeviceRecordingsDal : MongoEntityRepositoryBase<DeviceRecords>, IDeviceRecordingsDal
    {
        public List<DeviceRecords> GetUsersDevice(string userId)
        {
            
                var users = GetAll("Devices");
                List<DeviceRecords> cihazlar = new List<DeviceRecords>();
                if (users == null) return null;
                foreach (var x in users)
                {
                    if (x.IsDelete == 0 && x.UserId == userId)
                    {
                        cihazlar.Add(x);
                    }
                }
            return cihazlar;
           
        }

        public void SaveRegistrationId(string userId, string regId, int deviceType)
        {
            DeviceRecords kayit = new DeviceRecords()
            {
                DeviceType = deviceType,
                UserId = userId,
                Reg = regId,
                IsDelete = 0,
                RegId = 1,
                Date = DateTime.Now
            };

            if(isUserHaveRegId(userId,regId,deviceType) > 0)
            {
                Add(kayit, "Devices");
            }
          

        }

        public int isUserHaveRegId(string userId, string regId, int deviceType)
        {
            try
            {
                var users = GetAll("Devices");
                List<DeviceRecords> kullanicilar = new List<DeviceRecords>();
                if (users == null) return 1;
                foreach (var x in users)
                {
                   if(x.Reg == regId && x.UserId == userId && x.DeviceType == deviceType)
                    {
                        return -1;
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {

                return -2;
            }
        }
    }
}
