using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Entity.Model
{
    public class AnnouncementsPushModel
    {
        public string token { get; set; }
        public string user { get; set; }
        public string title { get; set; }
        public string message { get; set; }
    }
}
