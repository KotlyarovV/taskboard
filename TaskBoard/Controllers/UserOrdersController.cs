using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult Delete(long orderId)
        {
            string login =  User.Identity.Name;
            _orderRepository.RemoveOrder(login, orderId);
            return Redirect("~/UserOrders/Index");
        }

        [Authorize(Roles = "user")]
        [HttpGet]
        public IActionResult Edit(long orderId)
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
        public IActionResult ChooseDoer(long orderId)
        {
            string login = User.Identity.Name;
            ViewBag.OrderId = orderId;
            if (_orderRepository.GetUserOrdersList(login)[orderId].DoerCandidates == null)
            {
                _orderRepository.GetUserOrdersList(login)[orderId].DoerCandidates = new List<string>();
            }
            return View(_orderRepository.GetUserOrdersList(login)[orderId].DoerCandidates);
        }

        [Authorize(Roles = "user")]
        public IActionResult ChooseDoerAction(long orderId, string doerLogin)
        {
            string login = User.Identity.Name;
            if (_orderRepository.GetUserOrdersList(login).ContainsKey(orderId))
            {
                if (!String.IsNullOrEmpty(_orderRepository.GetUserOrdersList(login)[orderId].Doer))
                {
                    ViewBag.Body = "Исполнитель уже выбран";
                    return View("Message");
                }
                
                _orderRepository.GetUserOrdersList(login)[orderId].Doer = doerLogin;
                _orderRepository.GetUserOrdersList(login)[orderId].DoerCandidates = new List<string>();
            }
            return Redirect("~/UserOrders/Index");
        }

        [Authorize(Roles = "user")]
        public IActionResult ApplyAsDoer(long orderId, string ownerLogin, string doerLogin)
        {
            //
            if (_orderRepository.GetUserOrdersList(ownerLogin).ContainsKey(orderId))
            {
                UserModel doer = _userRepository.GetUser(doerLogin);
                if (doer != null)
                {
                    if (_orderRepository.GetUserOrdersList(ownerLogin)[orderId].DoerCandidates == null)
                    {
                        _orderRepository.GetUserOrdersList(ownerLogin)[orderId].DoerCandidates = new List<string> { doerLogin };
                    }
                    else
                    {
                        List<string> doers = _orderRepository.GetUserOrdersList(ownerLogin)[orderId].DoerCandidates.ToList();
                        doers.Add(doerLogin);
                        _orderRepository.GetUserOrdersList(ownerLogin)[orderId].DoerCandidates = doers;
                    }
                }
                
            }
                return Redirect("~/TaskBoard/Index");
        }

        [Authorize(Roles = "user")]
        public IActionResult WorkBoard(long orderId, string owner)
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
        public IActionResult Accept(long orderId)
        {
            string login = User.Identity.Name;
            _orderRepository.GetUserOrdersList(login)[orderId].IsCompleted = true;
            return Redirect("/userOrders/Workboard?orderId=" + orderId + "&owner=" + login);
        }

        [Authorize(Roles = "user")]
        public IActionResult OrdersToDo()
        {
            string login = User.Identity.Name;
            _orderRepository.GetOrdersList(0).Where(orderModel => orderModel.Doer.Equals(login));
            return View(_orderRepository.GetOrdersList(0).Where(orderModel => !String.IsNullOrEmpty(orderModel.Doer) && orderModel.Doer.Equals(login)));
        }
    }
}