using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Entity.Concreate
{
    public class LoginTemp : IEntity 
    {
        public ObjectId Id { get; set; }
        [BsonElement("email")]
        public string email { get; set; }
        [BsonElement("adsoyad")]
        public string adsoyad { get; set; }
        [BsonElement("sifre")]
        public string sifre { get; set; }
        [BsonElement("donensifre")]
        public string donensifre { get; set; }
    }
}
