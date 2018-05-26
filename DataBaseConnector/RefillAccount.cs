using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataBaseConnector
{
    public class RefillAccount
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Display(Name = "value")]
        public double Value { get; set; }
        [Display(Name = "date")]
        public DateTime Date { get; set; }
    }
}
