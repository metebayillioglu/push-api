using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Push.Business.Abstract
{
    public interface ISendPushService
    {
        Task<int> SendAndroid(string regId, string title, string body);
       void SendIos(string regId, string baslik, string mesaj);
        int SendPushNotification(string userId, string title, string message);
    }
}
