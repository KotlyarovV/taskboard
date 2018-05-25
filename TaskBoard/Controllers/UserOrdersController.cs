using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBoard.Data;
using TaskBoard.Models;

namespace TaskBoard.Controllers
{
    public class UserOrdersController : Controller
    {

        private readonly IOrderRepository _orderRepository;

        public UserOrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [Authorize]
        public IActionResult Index()
        {
            string login = User.Identity.Name;
            ViewData["login"] = login;
            return View(_orderRepository.GetUserOrdersList(login));
        }

        [Authorize]
        [HttpGet()]
        public IActionResult Manage(long orderId)
        {
            ViewBag.OrderId = orderId; // todo: for debug
            string login =  User.Identity.Name;
            return View(_orderRepository.GetUserOrdersList(login));
        }

        // Possible CSRF leak
        [Authorize]
        public IActionResult Delete(long orderId)
        {
            string login =  User.Identity.Name;
            _orderRepository.RemoveOrder(login, orderId);
            return Redirect("~/UserOrders/Index");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(long orderId)
        {
            string login = User.Identity.Name;
            return View(_orderRepository.GetUserOrdersList(login)[orderId]);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(OrderModel orderModel)
        {
            string login = User.Identity.Name;
            if (_orderRepository.GetUserOrdersList(login).ContainsKey(orderModel.Id))
            {
                _orderRepository.UpdateOrder(login, orderModel);
            }
            return Redirect("~/UserOrders/Edit");
        }

        [Authorize]
        public IActionResult ChooseDoer(long orderId)
        {
            string login = User.Identity.Name;
            return View(_orderRepository.GetUserOrdersList(login)[orderId]);
        }

        [Authorize]
        [HttpPost]
        public IActionResult ChooseDoer(long orderId, string doerLogin)
        {
            string login = User.Identity.Name;
            if (_orderRepository.GetUserOrdersList(login).ContainsKey(orderId))
            {
                _orderRepository.GetUserOrdersList(login)[orderId].Doer = doerLogin;
            }
            return Redirect("~/UserOrders/Index");
        }

        [Authorize]
        public IActionResult WorkBoard(long orderId)
        {
            string login = User.Identity.Name;
            return View(_orderRepository.GetUserOrdersList(login)[orderId]);
        }
    }
}