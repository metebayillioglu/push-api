using Push.Entity.Concreate;
using Push.Entity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Business.Abstract
{
  public  interface IRabbitMqService
    {
        bool AddPushToRabbitMq(PushModel model);
    }
}
