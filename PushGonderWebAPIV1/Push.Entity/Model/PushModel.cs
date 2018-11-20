using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Entity.Model
{
  public  class PushModel
    {
        public string RegId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int DeviceType { get; set; }
    }
}
