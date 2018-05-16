using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBoard.Data;

namespace TaskBoard.Controllers
{
    [Authorize]
    public class MoneyController : Controller
    {
        private readonly IFinanceRepository _financeRepository;

        public MoneyController(IFinanceRepository financeRepository)
        {
            _financeRepository = financeRepository;
        }

        public IActionResult Index()
        {
            var account = _financeRepository.GetAccount(User.Identity.Name);
            ViewBag.Balance = account.Balance;

            DateTime monthAgo = DateTime.Now.AddMonths(-1);
            ViewBag.MonthCosts = account.SpentSince(monthAgo);
            var earning = account.EarnSince(monthAgo);
            var replenishment = account.RechargeSince(monthAgo);
            ViewBag.MonthIncome = replenishment + earning;
            return View();
        }
    }
}