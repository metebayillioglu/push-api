using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Entity.Model
{
    public class AnnouncementsReturnModel
    {
        public string AnnoucncementId { get; set; }
     
        public string Title { get; set; }
     
        public string Message { get; set; }

        public string Date { get; set; }

        public string UserId { get; set; }
    }
}
