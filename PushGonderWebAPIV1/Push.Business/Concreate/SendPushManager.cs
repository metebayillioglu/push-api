using FCM.Net;
using Push.Business.Abstract;
using Push.DataAccess.Abstract;
using Push.Entity.Concreate;
using Push.Entity.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Push.Business.Concreate
{
  public  class SendPushManager : ISendPushService
    {
        private IDeviceRecordingsDal _iDeviceRecordingsDal;
        private IRabbitMqService _iRabbitMqService;
        public SendPushManager(IDeviceRecordingsDal iDeviceRecordingsDal, IRabbitMqService iRabbitMqService)
        {
            _iDeviceRecordingsDal = iDeviceRecordingsDal;
            _iRabbitMqService = iRabbitMqService;
        }

        
        public async Task<int> SendAndroid(string regId, string title, string body)
        {
            var registrationId = regId;
            var senderId = "";
           
            //You can get the server Key by accessing the url/ Você pode obter a chave do servidor acessando a url 
            //https://console.firebase.google.com/project/MY_PROJECT/settings/cloudmessaging";
            //  using (var sender = new Sender("AAAAJ8BsJvM:APA91bGQ5p1KRavILoNnhqxUB8FNTuUvcU7ddDytAEOYEfOHzIF1Z41aqWrpGNsYD82yhBICYVMhDYWZBMwSd4xxBAe2zYuQgVTK_nv1xj3cnhM5orc2NLyQ_TfdIK73Ri8gwbf0UZvx"))
            using (var sender = new Sender(senderId))

            {
                var message = new Message
                {
                    RegistrationIds = new List<string> { registrationId },
                    RestrictedPackageName = "com.uzar.pushforever",
                    ContentAvailable = true,
                    Notification = new Notification
                    {
                        Title = title,
                        Body = body,
                        Badge = "10",

                    }
                };
                var result = await sender.SendAsync(message);
                Console.WriteLine($"Success: {result.MessageResponse.Success}");

                //var json = "{\"notification\":{\"message\":\"test message\",\"icerik\":\"test like a charm!\"},\"to\":\"" + regId + "\"}";
                // var result = await sender.SendAsync(json);
                Console.WriteLine($"Success: {result.MessageResponse.Success}");
                return 1;
            }
        }

        public async void SendIos(string regId, string title, string message)
        {
            

        }

        public void SendPushNotification(string userId, string title, string message)
        {
            List<DeviceRecords> devices = new List<DeviceRecords>();
            devices = _iDeviceRecordingsDal.GetUsersDevice(userId);

            foreach (var x in devices)
            {
                var model = new PushModel()
                {
                    Message = message,
                    Title = title,
                    RegId = x.Reg,
                    DeviceType = x.DeviceType
                };
              
             bool result =   _iRabbitMqService.AddPushToRabbitMq(model);
                //rabbit error
                if (!result)
                {
                    if (x.DeviceType == 1)
                    {
                        SendAndroid(x.Reg, title, message);
                    }
                    else
                    {
                        SendIos(x.Reg, title, message);
                    }
                }

            }

           
        }
    }
}
