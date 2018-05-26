using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DataBaseConnector;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataBaseConnector
{
    public class FinancialAccount
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Display(Name = "owner")]
        public string Owner { get; set; }
        [Display(Name = "balance")]
        public double Balance { get; set; }
        [Display(Name = "operations")]
        public IEnumerable<FinancialOperation> Operations { get; set; }
        [Display(Name = "refills")]
        public IEnumerable<RefillAccount> RefillAccounts { get; set; }
        private double GetBalanceChangeSince(Func<FinancialOperation, bool> checkOperation, DateTime date)
        {
            if (Operations == null || !Operations.Any())
            {
                return 0;
            }

            return Operations
                .Where(operation => checkOperation(operation) && operation.Date >= date)
                .Sum(operation => operation.Amount);
        }

        public double EarnSince(DateTime date) => 
            GetBalanceChangeSince(operation => operation.LoginTo == Owner, date);

        public double SpentSince(DateTime date) =>
            GetBalanceChangeSince(operation => operation.LoginFrom == Owner, date);

        public double RechargeSince(DateTime date)
        {
            if (RefillAccounts == null || !RefillAccounts.Any())
            {
                return 0;
            }
            return RefillAccounts.Where(refill => refill.Date >= date).Sum(refill => refill.Value);
        }
    }
}
