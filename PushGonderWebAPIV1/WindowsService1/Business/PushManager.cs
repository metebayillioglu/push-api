using FCM.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WindowsService1.Model;

namespace WindowsService1.Business
{
    class PushManager
    {
        private static PushManager _pushManager;
        private static object obj=new object();
        public static PushManager CreateSingleton()
        {
            lock (obj)
            {
                if (_pushManager == null)
                {
                    _pushManager = new PushManager();
                }
                return _pushManager;
            }
       
        }
        private PushManager()
        {

        }

        public async Task<int> SendAndroid(string regId, string title, string body)
        {
            var registrationId = regId;
            var senderId = "AAAAU6rwU0c:APA91bHlPIPqa2yOqmdmf-9IjQ0-_yJPfRk-OzzruEFz-xhPqAvQF2N8LT4wJO-JiLYPXeHwgQUbsggbXobS1kI7uAxYJSXgTCA41PYzMLBX8hj9TNKmjDbU8cI28gNWtfR40HoJ97aB";
            // "dwt1slN1SH8:APA91bGWbiyhsSHy-QeYQhRHjELoNOjJzk8TEL0kIS2CAZ3XheQIEdd-Q8DSH4t-5bMMc36qWqdGOJhlPw_QkYFLWtdiCpdos8cORINVrAbZBVUlp9LN6eX4mXZPrCgwQya0Jtchlz7Q";

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

        public async void KisiyePushGonderIos(string regId, string baslik, string mesaj)
        {
            Console.WriteLine("KisiyePushGonderIos çalışmıyor");

            UTF8Encoding enc = new UTF8Encoding();

            string data = "[\"some.data\"]";

            WebRequest request = WebRequest.Create("http://push.pushforever.online/pushkisi.php?regid=" + regId + "&message=" + mesaj + "&title=" + baslik + "&appID=1112&isTest=1&&duyuruID=1&olayID=1");
            //WebRequest request = WebRequest.Create("http://push.kongremobiluygulama.com/pushserver/pushkisi.php?regid=" + regId + "&title=" + baslik + "&message=" + mesaj + "&appID=" + etkinlikId + "&duyuruID=1&isTest=0&olayID=1");
            request.Method = "POST";
            request.ContentType = "application/json";

            Stream dataStream = await request.GetRequestStreamAsync();
            dataStream.Write(enc.GetBytes(data), 0, data.Length);

            WebResponse wr = await request.GetResponseAsync();
            Stream receiveStream = wr.GetResponseStream();
            StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
            string content = reader.ReadToEnd();
        }

        public async void PushGonder(PushModel model)
        {

            if (model.DeviceType == 1)
            {
               await  SendAndroid(model.RegId, model.Title, model.Message);
            }
            else
            {
                KisiyePushGonderIos(model.RegId, model.Title, model.Message);
            }

        }

    }
}
