using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBaseConnector;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBoard.Data;
using TaskBoard.Models;
using TaskBoard.Models.User;

namespace TaskBoard.Controllers
{
    public class UserOrdersController : Controller
    {

        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;

        public UserOrdersController(IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        [Authorize(Roles = "user")]
        public IActionResult Index()
        {
            string login = User.Identity.Name;
            return View(_orderRepository.GetUserOrdersList(login).Values);
        }

        // Possible CSRF leak
        [Authorize(Roles = "user")]
        public IActionResult Delete(string orderId)
        {
            string login =  User.Identity.Name;
            _orderRepository.RemoveOrder(login, orderId);
            return Redirect("~/UserOrders/Index");
        }

        [Authorize(Roles = "user")]
        [HttpGet]
        public IActionResult Edit(string orderId)
        {
            string login = User.Identity.Name;
            return View(_orderRepository.GetUserOrdersList(login)[orderId]);
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public IActionResult Edit(OrderModel orderModel)
        {
            string login = User.Identity.Name;
            if (_orderRepository.GetUserOrdersList(login).ContainsKey(orderModel.Id))
            {
                _orderRepository.UpdateOrder(login, orderModel);
            }
            return Redirect("~/UserOrders/Edit/"+orderModel.Id);
        }

        [Authorize(Roles = "user")]
        [Route("[controller]/[action]/{*orderId}")]
        public IActionResult ChooseDoer(string orderId)
        {
            string login = User.Identity.Name;
            var orders = _orderRepository.GetUserOrdersList(login);
            ViewBag.OrderId = orderId;
            if (orders[orderId].DoerCandidates == null)
            {
                orders[orderId].DoerCandidates = new List<string>();
            }
            return View(orders[orderId].DoerCandidates);
        }

        [Authorize(Roles = "user")]
        public IActionResult ChooseDoerAction(string orderId, string doerLogin)
        {
            string login = User.Identity.Name;
            var orders = _orderRepository.GetUserOrdersList(login);
            if (orders.ContainsKey(orderId))
            {
                if (!String.IsNullOrEmpty(orders[orderId].Doer))
                {
                    ViewBag.Body = "Исполнитель уже выбран";
                    return View("Message");
                }
                var order = _orderRepository.GetUserOrdersList(login)[orderId];
                order.Doer = doerLogin;
                order.DoerCandidates = new List<string>();
                _orderRepository.UpdateOrder(login, order);
            }
            return Redirect("~/UserOrders/Index");
        }

        [Authorize(Roles = "user")]
        public IActionResult ApplyAsDoer(string orderId, string ownerLogin, string doerLogin)
        {
            var ordersOfUser = _orderRepository.GetUserOrdersList(ownerLogin);
            if (ordersOfUser.ContainsKey(orderId))
            {
                var order = ordersOfUser[orderId];
                var doer = _userRepository.GetUser(doerLogin);
                if (doer != null)
                {
                    if (order.DoerCandidates == null)
                    {
                        order.DoerCandidates = new List<string> { doerLogin };
                    }
                    else
                    {
                        List<string> doers = order.DoerCandidates.ToList();
                        doers.Add(doerLogin);
                        order.DoerCandidates = doers;
                    }
                    _orderRepository.UpdateOrder(ownerLogin, order);
                }
                
            }
            return Redirect("~/TaskBoard/Index");
        }

        [Authorize(Roles = "user")]
        public IActionResult WorkBoard(string orderId, string owner)
        {
            if (String.IsNullOrEmpty(_orderRepository.GetUserOrdersList(owner)[orderId].Doer))
            {
                ViewBag.Body = "Исполнитель не выбран.";
                return View("Message");
            }
            string login = User.Identity.Name;
            if (login.Equals(owner))
            {
                ViewBag.isOwner = true;
            }
            return View(_orderRepository.GetUserOrdersList(owner)[orderId]);
        }

        [Authorize(Roles = "user")]
        public IActionResult Accept(string orderId)
        {
            var login = User.Identity.Name;
            var order = _orderRepository.GetUserOrdersList(login)[orderId];
            order.IsCompleted = true;
            _orderRepository.UpdateOrder(login, order);
            return Redirect("/userOrders/Workboard?orderId=" + orderId + "&owner=" + login);
        }

        [Authorize(Roles = "user")]
        public IActionResult OrdersToDo()
        {
            var login = User.Identity.Name;
            var ordersFromR = _orderRepository.GetOrdersList(0).ToList();
            var orders = _orderRepository.GetOrdersList(0).Where(orderModel => orderModel.Doer == login).ToList();
            return View(orders);
        }
    }
}