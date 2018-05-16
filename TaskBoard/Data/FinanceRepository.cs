using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskBoard.Models.Finance;

namespace TaskBoard.Data
{
    public class FinanceRepository : IFinanceRepository
    {
        private List<FinancialAccount> _accounts = new List<FinancialAccount>();
        public IFinanceRepository AddAccount(string owner)
        {
            var account = new FinancialAccount
            {
                Balance = 0,
                Operations = new List<FinancialOperation>() { new FinancialOperation() { Date = DateTime.Now, LoginFrom = owner, LoginTo = "ff", Amount = 1} }
            };
            account.Owner = owner;
            _accounts.Add(account);
            return this;
        }

        public FinancialAccount GetAccount(string owner)
        {
            return _accounts.First(acc => acc.Owner == owner);
        }
    }
}
