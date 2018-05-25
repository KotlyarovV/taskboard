using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaskBoard.Models.Enums;

namespace TaskBoard.Models
{
    public class OrderModel
    {
        public long Id { get; set; }
        public string Owner { get; set; }
        public string Header { get; set; }
        public string Doer { get; set; }
        public IEnumerable<string> DoerCandidates { get; set; }
        public bool IsCompleted { get; set; } = false;

        [Required]
        public OrderTheme Theme { get; set; }
        public string Description { get; set; }
        public string[] FormFilesLinks { get; set; }
        public DateTime Deadline { get; set; }

        [Range(minimum: 0, maximum:50000)]
        public double Value { get; set; }

        [Required]
        public ImplementationMethod ImplementationMethod { get; set; }

        [Required]
        public ConnectionMethod ConnectionMethod { get; set; }
    }
}
