using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataBaseConnector;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataBaseConnector
{
    public class OrderModel
    {
        [BsonId]
        public string Id { get; set; }
        [Display(Name = "owner")]
        public string Owner { get; set; }
        [Display(Name = "header")]
        public string Header { get; set; }
        [Display(Name = "doer")]
        public string Doer { get; set; }
        [Display(Name = "doercandidates")]
        public IEnumerable<string> DoerCandidates { get; set; }
        [Display(Name = "iscompleted")]
        public bool IsCompleted { get; set; } = false;

        [Required]
        [Display(Name = "ordertheme")]
        public OrderTheme Theme { get; set; }
        [Display(Name = "description")]
        public string Description { get; set; }
        [Display(Name = "filelinks")]
        public string[] FormFilesLinks { get; set; }
        [Display(Name = "date")]
        public DateTime Deadline { get; set; }

        [Range(minimum: 0, maximum:50000)]
        [Display(Name = "value")]
        public double Value { get; set; }

        [Required]
        [Display(Name = "ImplementationMethod")]
        public ImplementationMethod ImplementationMethod { get; set; }

        [Required]
        [Display(Name = "ConnectionMethod")]
        public ConnectionMethod ConnectionMethod { get; set; }
    }
}
