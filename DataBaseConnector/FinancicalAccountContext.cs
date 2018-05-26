using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace DataBaseConnector
{
    public class FinancicalAccountContext
    {
        IMongoDatabase database; // база данных

        public FinancicalAccountContext()
        {
            var connectionString = "mongodb+srv://kotlvit:qwery@reports-qowm3.mongodb.net/test?retryWrites=true";
            var connection = new MongoUrlBuilder(connectionString);
            var client = new MongoClient(connectionString);
            database = client.GetDatabase(connection.DatabaseName);
        }

        private IMongoCollection<FinancialAccount> Finanses => database.GetCollection<FinancialAccount>("finance");

        public void AddAccount(string owner)
        {
            var account = new FinancialAccount
            {
                Balance = 0,
                Operations = new List<FinancialOperation>() {}
            };
            account.Owner = owner;
            Finanses.InsertOne(account);
        }

        public FinancialAccount GetAccount(string owner)
        {
            return Finanses
                .AsQueryable()
                .FirstOrDefault(u => u.Owner == owner);
        }

        
    }
}
