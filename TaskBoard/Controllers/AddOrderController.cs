using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskBoard.Data;
using TaskBoard.Models;

namespace TaskBoard.Controllers
{
    [Authorize]
    public class AddOrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IFileRepository _fileRepository;
        public AddOrderController(IOrderRepository orderRepository, IFileRepository fileRepository)
        {
            _orderRepository = orderRepository;
            _fileRepository = fileRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> AddOrder(OrderModel order, IFormFile[] formFiles)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/addorder/index");
            }

            ViewBag.Body = "Заказ был успешно принят";
            order.Owner = User.Identity.Name;
            if (formFiles != null)
            {
                var saveTasks = formFiles.Select(file => _fileRepository.SaveFile(file)).ToList();
                await Task.WhenAll(saveTasks);
                order.FormFilesLinks = saveTasks.Select(task => task.Result).ToArray();
            }
            _orderRepository.SaveOrder(User.Identity.Name, order);
            return View("Message");
        }
    }
}