using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1.Business
{
    class SendMail
    {
        private SendMail()
        {

        }
        private static SendMail _mailGonder;
        public static SendMail CreateSingleton()
        {
            return _mailGonder ?? (_mailGonder = new SendMail());
        }
        public void Send(string subject, string body, string to)
        {
            try
            {

                MailMessage mesaj = new MailMessage();
                mesaj.From = new MailAddress("info@pushforever.online");
                mesaj.To.Add(to);
                mesaj.Subject = subject;
                mesaj.Body = body;

                mesaj.IsBodyHtml = true; // giden mailin içeriği html olmasını istiyorsak true kalması lazım
                SmtpClient client = new SmtpClient("smtp.yandex.ru", 587);
                client.Credentials = new NetworkCredential("info@pushforever.online", "Mete1905mete");
                client.EnableSsl = true;
                client.Send(mesaj);


              

                var logger = WriteEventLog.CreateSingleton();
                logger.LogEkle("email send => " + to, EventLogEntryType.Information);
               
            }
            catch (Exception ex)
            {
                var logger = WriteEventLog.CreateSingleton();
                logger.LogEkle("email send error=> " + ex.ToString(), EventLogEntryType.Error);
             
            }
        }
    }
}
