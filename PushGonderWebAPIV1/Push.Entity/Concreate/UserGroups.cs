using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Entity.Concreate
{
    public class UserGroups: IEntity
    {

        public ObjectId Id { get; set; }
        [BsonElement("GId")]
        public int GId { get; set; }
        [BsonElement("GroupId")]
        public int GroupId { get; set; }
        [BsonElement("UserId")]
        public int UserId { get; set; }
        [BsonElement("isDelete")]
        public int isDelete { get; set; }

    }
}
