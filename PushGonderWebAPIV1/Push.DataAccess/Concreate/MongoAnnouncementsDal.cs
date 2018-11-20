
using Push.Entity.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Push.DataAccess.Abstract;
using Push.Entity.Model;
using Push.Core.Crypto;

namespace Push.DataAccess.Concreate
{
    public class MongoAnnouncementsDal : MongoEntityRepositoryBase<Announcements>, IAnnouncementsDal
    {
        private ICryptyoService _icryptyoService;
        public MongoAnnouncementsDal(ICryptyoService icryptyoService)
        {
            _icryptyoService = icryptyoService;
        }

        public int DeleteAnnouncements(string announcementId, string userId)
        {
         Announcements duyuru =   GetAnnouncementsFromId(announcementId);
            if(duyuru.UserId == userId)
            {
                DeleteAnnouncements(duyuru);
                return 1;
            }
            else
            {
                return -2;
            }
            
        }

        public int AddAnnouncements(Announcements duyuru)
        {
            try
            {

                duyuru.AnnouncementId = _icryptyoService.Base64Encode(_icryptyoService.Encrypt(ReturnAnnouncementIdForNewAnnoucement().ToString()));

                Add(duyuru, "Announcements");
                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

     


        public List<AnnouncementsReturnModel> GetAnnouncenmentsFromUserId(string kullaniciId)
        {

            List<AnnouncementsReturnModel> announcementsList = new List<AnnouncementsReturnModel>();
            var annouInfo = _database.GetCollection<Announcements>("Announcements");

            foreach (Announcements ano in annouInfo.FindAll())
            {
                if (ano.UserId == kullaniciId && ano.isDelete == 0)
                    announcementsList.Add(new AnnouncementsReturnModel()
                    {
                        Title = ano.Title,
                        Message = ano.Message,
                        AnnoucncementId = ano.AnnouncementId,
                        UserId = ano.UserId,
                        Date = ano.Date.ToShortDateString()
                    });
            }

            return announcementsList;

        }

        public List<Announcements> GetAllAnnouncements()
        {
            try
            {
                var users = GetAll("Announcements");
                List<Announcements> announcemenets = new List<Announcements>();
                if (users == null) return null;
                foreach (var x in users)
                {
                    announcemenets.Add(x);
                }
                return announcemenets;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public int ReturnAnnouncementIdForNewAnnoucement()
        {
            
            List<Announcements> users = GetAllAnnouncements();
            if (users.Count == 0)
                return 1;
            foreach(var x in users)
            {
                x.AnnouncementNo = Convert.ToInt32(_icryptyoService.Decrypt(_icryptyoService.Base64Decode(x.AnnouncementId)));
            }
            users = users.OrderByDescending(t => t.AnnouncementNo).ToList();
            return users.First().AnnouncementNo + 1;
          
        }
    }
}
