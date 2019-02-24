using Newtonsoft.Json;
using Push.Business.Abstract;
using Push.Entity.Concreate;
using Push.Entity.Model;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Business.Concreate
{
    public class RabbitMqManager : IRabbitMqService
    {
        public bool AddPushToRabbitMq(PushModel model)
        {
            try
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

                    string message = JsonConvert.SerializeObject(model);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "PushListener",
                                         basicProperties: null,
                                         body: body);
                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
            

        }
    }
}
