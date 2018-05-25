using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskBoard.Models;

namespace TaskBoard.Data
{
    public class OrderRepository : IOrderRepository
    {
        private Dictionary<string, Dictionary<long, OrderModel>> orders = new Dictionary<string, Dictionary<long, OrderModel>>();
        public IOrderRepository SaveOrder(string owner, OrderModel orderModel)
        {
            if (orders.ContainsKey(owner))
            {
                if (orders[owner] != null && orders[owner].Count != 0)
                    orders[owner].Add(orderModel.Id, orderModel);
                else
                    orders[owner] = new Dictionary<long, OrderModel>{ { orderModel.Id, orderModel } };
            }
            else
            {
                orders.Add(owner, new Dictionary<long, OrderModel> { { orderModel.Id, orderModel } });
            }
            
            return this;
        }

        public IEnumerable<OrderModel> GetOrdersList(int listNumber)
        {
            return orders.Values.Skip((listNumber - 1) * 100).Take(100).SelectMany(z => z.Values);
        }

        public bool RemoveOrder(string owner, long orderId)
        {
            bool wasDeleted = false;
            if (orders.ContainsKey(owner) && orders[owner].ContainsKey(orderId))
            {
                orders[owner].Remove(orderId);
                wasDeleted = true;
            }
            return wasDeleted;
        }

        public bool UpdateOrder(string owner, OrderModel orderModel)
        {
            bool wasUpdated = false;
            if (orders.ContainsKey(owner) && orders[owner].ContainsKey(orderModel.Id))
            {
                orders[owner][orderModel.Id] = orderModel;
                wasUpdated = true;
            }
            return wasUpdated;
        }

        public Dictionary<long, OrderModel> GetUserOrdersList(string owner)
        {
            if (!orders.ContainsKey(owner)) return new Dictionary<long, OrderModel>();
            return orders[owner];
        }
    }
}
