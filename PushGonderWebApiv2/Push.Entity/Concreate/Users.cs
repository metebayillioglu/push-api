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
    public class Users:IEntity
    {
        public ObjectId Id { get; set; }
        [BsonElement("UserId")]
        public string UserId { get; set; }
        [BsonElement("NameSurname")]
        public string NameSurname { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; }
        [BsonElement("isDelete")]
        public int isDelete { get; set; }
        [NotMapped]
        public int UserNo { get; set; }

    }
}
