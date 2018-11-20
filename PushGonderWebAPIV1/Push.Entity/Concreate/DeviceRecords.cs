using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Entity.Concreate
{
    public class DeviceRecords: IEntity
    {
        
        public ObjectId Id { get; set; }
        [BsonElement("RegId")]
        public int RegId { get; set; }
        [BsonElement("Reg")]
        public string Reg { get; set; }
        [BsonElement("UserId")]
        public string UserId { get; set; }
        [BsonElement("IsDelete")]
        public int IsDelete { get; set; }
        [BsonElement("DeviceType")]
        public int DeviceType { get; set; }
        [BsonElement("Date")]
        public DateTime Date{ get; set; }


    }
}
