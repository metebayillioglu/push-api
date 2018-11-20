using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsService1.Business;
using WindowsService1.Model;

namespace WindowsService1
{
   public class PushListener
    {
        public void Listener()
        {


            int sayac = 0;
            try
            {
                new Thread(() =>
                {
                    var factory = new ConnectionFactory() { HostName = "localhost" };

                    using (IConnection connection = factory.CreateConnection())
                    using (IModel channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: "PushListener",
                                                 durable: false,
                                                 exclusive: false,
                                                 autoDelete: false,
                                                 arguments: null);

                        var consumer = new QueueingBasicConsumer(channel);

                        //   channel.BasicConsume(queue: "AottMakaleSayac",
                        //    consumer: consumer);

                        channel.BasicConsume(queue: "PushListener",
                            autoAck: true,
                            consumer: consumer);

                        while (true)
                        {
                            var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                            var body = ea.Body;
                            var message = Encoding.UTF8.GetString(body);
                            PushModel pushModel = JsonConvert.DeserializeObject<PushModel>(message);


                            var pushManager = PushManager.CreateSingleton();
                            pushManager.PushGonder(pushModel);

                            var asd = WriteEventLog.CreateSingleton();
                            asd.LogEkle("push iletildi" + pushModel.RegId, EventLogEntryType.Information);



                        }
                    }
                }).Start();
            }
            catch (Exception ex)
            {
                // string error = ex.Message;
                // Console.WriteLine("hata => "+ex.ToString());
                var mail = SendMail.CreateSingleton();
                mail.Send("Push Listener => ", ex.ToString(), "metebayillioglu@gmail.com");

                var log = WriteEventLog.CreateSingleton();
                log.LogEkle("Push Listener=> " + ex.ToString(), EventLogEntryType.Error);


            }
        }
    }
}
