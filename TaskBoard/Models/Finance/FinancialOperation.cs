using System;

namespace TaskBoard.Models.Finance
{
    public class FinancialOperation
    {
        public string LoginFrom { get; set; }
        public string LoginTo { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
    }
}
