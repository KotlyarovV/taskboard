using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBaseConnector;

namespace TaskBoard.Data
{
    public class FinanceRepository : IFinanceRepository
    {
        private FinancicalAccountContext _accountContext;
        public FinanceRepository(FinancicalAccountContext financicalAccountContext)
        {
            _accountContext = financicalAccountContext;
        }


        public IFinanceRepository AddAccount(string owner)
        {
            _accountContext.AddAccount(owner);
            return this;
        }

        public FinancialAccount GetAccount(string owner)
        {
            var account = _accountContext.GetAccount(owner);
            if (account == null)
            {
                AddAccount(owner);
            }

            return _accountContext.GetAccount(owner);
        }
    }
}
