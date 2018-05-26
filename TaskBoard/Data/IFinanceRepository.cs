using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBaseConnector;

namespace TaskBoard.Data
{
    public interface IFinanceRepository
    {
        IFinanceRepository AddAccount(string owner);
        FinancialAccount GetAccount(string owner);
    }
}
