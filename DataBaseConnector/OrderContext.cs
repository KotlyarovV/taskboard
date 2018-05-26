using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataBaseConnector
{
    public class OrderContext
    {
        IMongoDatabase database; // база данных

        public OrderContext()
        {
            var connectionString = "mongodb+srv://kotlvit:qwery@reports-qowm3.mongodb.net/test?retryWrites=true";
            var connection = new MongoUrlBuilder(connectionString);
            var client = new MongoClient(connectionString);
            database = client.GetDatabase(connection.DatabaseName);
        }

        private IMongoCollection<OrderModel> Orders => database.GetCollection<OrderModel>("orders");

        public string SaveOrder(OrderModel order)
        {
            order.Id = Guid.NewGuid().ToString();
            Orders.InsertOne(order);
            return order.Id;
        }

        public IEnumerable<OrderModel> GetOrderList(int listNumber)
        {
            return Orders.AsQueryable().Skip((listNumber) * 100).Take(100);
        }

        public Dictionary<string, OrderModel> GetUserOrdersList(string owner)
        {
            var order = Orders.AsQueryable().FirstOrDefault(o => o.Owner == owner);
            if (order == null)
            {
                return new Dictionary<string, OrderModel>();
            }

            return Orders
                .AsQueryable()
                .Where(o => o.Owner == owner)
                .ToDictionary(o => o.Id, o => o);
        }

        public bool RemoveOrder(string owner, string id)
        {
            Orders.DeleteOne(new BsonDocument("_id", new ObjectId(id)));
            return true;
        }

        public bool UpdateOrder(string owner, OrderModel orderModel)
        {
            var filter = Builders<OrderModel>.Filter.Eq(o => o.Id, orderModel.Id);
            Orders.ReplaceOne(filter, orderModel);
            return true;
        }
        

    }
}
