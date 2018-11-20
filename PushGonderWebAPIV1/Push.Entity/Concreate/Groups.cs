using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Entity.Concreate
{
    public class Groups:IEntity
    {
        public ObjectId Id { get; set; }
        [BsonElement("GroupId")]
        public int GroupId { get; set; }
        [BsonElement("GroupCode")]
        public string GroupCode { get; set; }
        [BsonElement("GroupName")]
        public string GroupName { get; set; }
        [BsonElement("isDelete")]
        public int isDelete { get; set; }

    }
}
