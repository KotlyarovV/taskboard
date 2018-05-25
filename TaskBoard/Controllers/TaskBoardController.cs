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
        [Authorize(Roles = "user")]
        public IActionResult Index()
        {
            ViewBag.UserLogin = User.Identity.Name;
            var role = User.FindFirst(ClaimTypes.Role).Value;
            ViewData["login"] = role + " " + User.Identity.Name;
            return View(_orderRepository.GetOrdersList(0).Where(orderModel => !orderModel.IsCompleted));
        }
    }
}
