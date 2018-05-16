using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskBoard.Models;

namespace TaskBoard.Data
{
    public class OrderRepository : IOrderRepository
    {
        private Dictionary<string, OrderModel> orders = new Dictionary<string, OrderModel>();
        public IOrderRepository SaveOrder(string owner, OrderModel orderModel)
        {
            orders.Add(owner, orderModel);
            return this;
        }

        public IEnumerable<OrderModel> GetOrdersList(int listNumber)
        {
            return orders.Values.Skip((listNumber - 1) * 100).Take(100);
        }
    }
}
