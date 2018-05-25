using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskBoard.Models;

namespace TaskBoard.Data
{
    public interface IOrderRepository
    {
        IOrderRepository SaveOrder(string owner, OrderModel orderModel);
        IEnumerable<OrderModel> GetOrdersList(int listNumber);
        Dictionary<long, OrderModel> GetUserOrdersList(string owner);
        bool RemoveOrder(string owner, long orderId);
        bool UpdateOrder(string owner, OrderModel orderModel);
    }
}
