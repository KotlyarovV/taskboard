using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBoard.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskBoard.Controllers
{
    public class TaskBoard : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public TaskBoard(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        // GET: /<controller>/
        [Authorize]
        public IActionResult Index()
        {
            var role = User.FindFirst(ClaimTypes.Role).Value;
            ViewData["login"] = role + " " + User.Identity.Name;
            return View(_orderRepository.GetOrdersList(0));
        }
    }
}
