using Push.Business.Abstract;
using Push.DataAccess.Abstract;
using Push.Entity.Concreate;
using Push.Entity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Business.Concreate
{
    public class AnnouncementsManager : IAnnouncementService
    {
        private IAnnouncementsDal _iAnnouncementsDal;
        public AnnouncementsManager(IAnnouncementsDal iAnnouncementsDal)
        {
            _iAnnouncementsDal = iAnnouncementsDal;
        }

        public int DeleteAnnouncements(string announcementId, string userId)
        {
            return _iAnnouncementsDal.DeleteAnnouncements(announcementId, userId);
        }

        public int AddAnnouncements(Announcements model)
        {

            return _iAnnouncementsDal.AddAnnouncements(model);
        }

        public List<AnnouncementsReturnModel> GetAnnouncenmentsFromUserId(string userId)
        {
            return _iAnnouncementsDal.GetAnnouncenmentsFromUserId(userId);
      
        }

        public List<Announcements> GetAllAnnouncements()
        {
            return _iAnnouncementsDal.GetAllAnnouncements();
        }

        public int ReturnAnnouncementIdForNewAnnoucement()
        {
            return _iAnnouncementsDal.ReturnAnnouncementIdForNewAnnoucement();
        }
    }
}
