using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1.Model
{
   public class PushModel
    {
        public string RegId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int DeviceType { get; set; }
    }
}
