using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBaseConnector;
using TaskBoard.Models;

namespace TaskBoard.Data
{
    public class OrderRepository : IOrderRepository
    {
       
        private OrderContext _orderContext;

        public OrderRepository(OrderContext orderContext)
        {
            _orderContext = orderContext;
        }


        public IOrderRepository SaveOrder(string owner, OrderModel orderModel)
        {
            orderModel.Owner = owner;
            _orderContext.SaveOrder(orderModel);
            return this;
        }

        public IEnumerable<OrderModel> GetOrdersList(int listNumber)
        {
            return _orderContext.GetOrderList(listNumber);
        }

        public Dictionary<string, OrderModel> GetUserOrdersList(string owner)
        {
            return _orderContext.GetUserOrdersList(owner);
        }

        public bool RemoveOrder(string owner, string orderId)
        {
            return _orderContext.RemoveOrder(owner, orderId);
        }

        public bool UpdateOrder(string owner, OrderModel orderModel)
        {
            _orderContext.UpdateOrder(owner, orderModel);
            return true;
        }
    }
}
