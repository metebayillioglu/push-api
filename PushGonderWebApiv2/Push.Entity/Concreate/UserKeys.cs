using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Entity.Concreate
{
    public class UserKeys:IEntity
    {
        public ObjectId Id{ get; set; }
        [BsonElement("UserId")]
        public string UserId { get; set; }
        [BsonElement("Key")]
        public string Key { get; set; }
        [BsonElement("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [NotMapped]
        public int KeyString { get; set; }
    }
}
