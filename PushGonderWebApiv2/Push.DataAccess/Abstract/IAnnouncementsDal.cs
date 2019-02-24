
using Push.Entity.Concreate;
using Push.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.DataAccess.Abstract
{
    public interface IAnnouncementsDal : IMongoReprository<Announcements>
    {
       
        int AddAnnouncements(Announcements duyuru);
        List<Announcements> GetAllAnnouncements();
        int ReturnAnnouncementIdForNewAnnoucement();
        List<AnnouncementsReturnModel> GetAnnouncenmentsFromUserId(string kullaniciId);
        int DeleteAnnouncements(string announcementId,string userId);
    }
}
