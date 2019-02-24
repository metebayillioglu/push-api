using Push.Business.Abstract;
using Push.Entity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Push.Business.Concreate
{
   public class MailManager : IMailService
    {
        string EmailSender = "***";
        string EmailPassword = "***";
        private INotesService _notesService;
        public MailManager(INotesService notesService)
        {
            _notesService = notesService;
        }
        public void SendEmail(string body, string subject, string to)
        {
            try
            {
                EmailSender = _notesService.GetNote(NotesEnum.EmailSender.ToString());
                EmailPassword = _notesService.GetNote(NotesEnum.EmailPassword.ToString());

                MailMessage mesaj = new MailMessage();
                mesaj.From = new MailAddress(EmailSender);
                mesaj.To.Add(to);
                mesaj.Subject = subject;
                mesaj.Body = body;

                mesaj.IsBodyHtml = true; 
                SmtpClient client = new SmtpClient("smtp.yandex.ru", 587);
                client.Credentials = new NetworkCredential(EmailSender, EmailPassword);
                client.EnableSsl = true;
                client.Send(mesaj);


            }
            catch (Exception ex)
            {



            }
        }
    }
}
