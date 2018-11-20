using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WindowsService1.Business;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
            if (!System.Diagnostics.EventLog.SourceExists("eventLogPush"))
                System.Diagnostics.EventLog.CreateEventSource("eventLogPush", "eventLogPush");

            eventLogPush.Source = "eventLogPush";
            eventLogPush.Log = "eventLogPush";
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                EventLog.ModifyOverflowPolicy(OverflowAction.OverwriteOlder, 1);
                var logger = WriteEventLog.CreateSingleton();
                logger.LogEkle("Service start...", EventLogEntryType.Information);

                

                var mail = SendMail.CreateSingleton();
                mail.Send("Push Notifvation service start", "Push Notifvation service start...", "mete@citius.technology");

                var service = new PushListener();
                service.Listener();
            
            

            }
            catch (Exception e)
            {
                var mail = SendMail.CreateSingleton();
                mail.Send("Push Notifvation service stopped 1 ", "Push Notifvation service stopped 1" + e.ToString(), "metebayillioglu@gmail.com");
            }
        }

        protected override void OnStop()
        {
            var log = WriteEventLog.CreateSingleton();
            log.LogEkle("Service stopped...", EventLogEntryType.Information);

           var mail = SendMail.CreateSingleton();
            mail.Send("Push Notifvation service stopped 2", "Push Notifvation service stopped 2", "metebayillioglu@gmail.com");
        }
    }
}
