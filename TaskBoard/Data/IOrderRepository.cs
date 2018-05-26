using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBaseConnector;
using TaskBoard.Models;

namespace TaskBoard.Data
{
    public interface IOrderRepository
    {
        IOrderRepository SaveOrder(string owner, OrderModel orderModel);
        IEnumerable<OrderModel> GetOrdersList(int listNumber);
        Dictionary<string, OrderModel> GetUserOrdersList(string owner);
        bool RemoveOrder(string owner, string orderId);
        bool UpdateOrder(string owner, OrderModel orderModel);
    }
}
