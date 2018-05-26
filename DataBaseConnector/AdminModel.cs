using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataBaseConnector
{
    public class AdminModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Display(Name = "login")]
        public string Login { get; set; }
        [Display(Name = "password")]
        public string Password { get; set; }
    }
}
