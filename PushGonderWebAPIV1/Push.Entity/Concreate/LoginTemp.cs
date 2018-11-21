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
        [BsonElement("namesurname")]
        public string namesurname { get; set; }
        [BsonElement("password")]
        public string password { get; set; }
        [BsonElement("returnpassword")]
        public string returnpassword { get; set; }
    }
}
