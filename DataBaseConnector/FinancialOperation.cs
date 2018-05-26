using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataBaseConnector
{
    public class FinancialOperation
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Display(Name = "loginFrom")]
        public string LoginFrom { get; set; }
        [Display(Name = "loginTo")]
        public string LoginTo { get; set; }
        [Display(Name = "date")]
        public DateTime Date { get; set; }
        [Display(Name = "amount")]
        public double Amount { get; set; }
    }
}
