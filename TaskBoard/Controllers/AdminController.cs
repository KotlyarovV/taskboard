using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBoard.Data;
using TaskBoard.Infrastucture;
using TaskBoard.Models;

namespace TaskBoard.Controllers
{
    public class AdminController : Controller
    {

        IAdminRepository _adminRepository;
        IOrderRepository _orderRepository;
        IUserRepository _userRepository;

        public AdminController(IAdminRepository adminRepository, IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _adminRepository = adminRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Auth(AdminModel model)
        {
            if (!_adminRepository.CheckRegistration(model))
                return Redirect("/Admin/");

            await HttpContext.LogIn(model.Login, "admin");
            return Redirect("/Admin/DashBoard/");
        }

        [Authorize(Roles = "admin")]
        public IActionResult DashBoard()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult Report(string reportType)
        {
            if (reportType == "orders")
            {
                ReportGenerators.Word.Export(_orderRepository.GetOrdersList(0));
            }
            if (reportType == "users")
            {
                ReportGenerators.Excel.Export(_userRepository.GetUsers());

            }
            return Redirect("/Admin/DashBoard/");
        }

        [Authorize(Roles = "admin")]
        public IActionResult AdminControl()
        {
            return View(_adminRepository.GetAdmins());
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult AddAdmin(AdminModel adminModel)
        {
            _adminRepository.Save(adminModel);
            return Redirect("/Admin/AdminControl/");
        }
    }
}