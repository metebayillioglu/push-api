using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1.Business
{
    class WriteEventLog
    {
        private static WriteEventLog _mads;

        private WriteEventLog()
        {

        }

        public static WriteEventLog CreateSingleton()
        {
            if (_mads == null)
            {
                _mads = new WriteEventLog();
            }

            return _mads;
        }
        public void LogEkle(string log, EventLogEntryType type)
        {
            
#if DEBUG
            Console.WriteLine(log);
#else
 using (EventLog eventLog = new EventLog("eventLogPush"))
            {
                eventLog.Source = "eventLogPush";
                eventLog.WriteEntry(log, type);
            }
#endif
        }


    }
}
