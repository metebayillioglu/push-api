using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Entity.Concreate
{
    public class Announcements:IEntity
    {

        public ObjectId Id { get; set; }

        [BsonElement("AnnouncementId")]
        public string AnnouncementId { get; set; }
        [BsonElement("Title")]
        public string Title { get; set; }
        [BsonElement("Message")]
        public string Message { get; set; }
        [BsonElement("isDelete")]
        public int isDelete { get; set; }
        [BsonElement("UserId")]
        public string UserId { get; set; }
        [BsonElement("Date")]
        public DateTime Date { get; set; }
        [BsonElement("Counter")]
        public int Counter { get; set; }

        [NotMapped]
        public int AnnouncementNo { get; set; }

    }
}
