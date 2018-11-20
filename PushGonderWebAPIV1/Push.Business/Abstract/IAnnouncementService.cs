using Push.Entity.Concreate;
using Push.Entity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Business.Abstract
{
    public interface IAnnouncementService
    {
        int AddAnnouncements(Announcements model);
        List<Announcements> GetAllAnnouncements();
        int ReturnAnnouncementIdForNewAnnoucement();
        List<AnnouncementsReturnModel> GetAnnouncenmentsFromUserId(string userId);
        int DeleteAnnouncements(string announcementId, string userId);
    }
}
