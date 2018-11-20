using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Business.Abstract
{
   public interface IMailService
    {
        void SendEmail(string body, string subject, string to);
    }
}
