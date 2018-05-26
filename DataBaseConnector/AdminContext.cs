using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace DataBaseConnector
{
    public class AdminContext
    {
        IMongoDatabase database; // база данных

        public AdminContext()
        {
            var connectionString = "mongodb+srv://kotlvit:qwery@reports-qowm3.mongodb.net/test?retryWrites=true";
            var connection = new MongoUrlBuilder(connectionString);
            var client = new MongoClient(connectionString);
            database = client.GetDatabase(connection.DatabaseName);
        }

        private IMongoCollection<AdminModel> Admins => database.GetCollection<AdminModel>("admins");

        public async Task<IEnumerable<AdminModel>> GetAdmins()
        {
            return await Admins.Find(FilterDefinition<AdminModel>.Empty).ToListAsync();
        }

        public bool CheckRegistration(string login, string password)
        {
            var user = Admins
                .AsQueryable()
                .FirstOrDefault(u => u.Login == login && u.Password == password);
            return user != null;
        }

        public string Save(AdminModel admin)
        {
            Admins.InsertOne(admin);
            return admin.Id;
        }

        public bool ExistenceVerification(string login)
        {
            return Admins.AsQueryable().Any(registration => registration.Login == login);
        }

        public AdminModel GetAdmin(string login)
        {
            return Admins
                .AsQueryable()
                .FirstOrDefault(u => u.Login == login);
        }

        public AdminModel Update(string login, AdminModel user)
        {
            var userFromBd = GetAdmin(login);

            var type = userFromBd.GetType();
            foreach (var propertyInfo in type.GetProperties())
            {
                var newUserProperty = propertyInfo.GetValue(user);
                if (newUserProperty != null)
                {
                    propertyInfo.SetValue(userFromBd, newUserProperty);
                }
            }

            var filter = Builders<AdminModel>.Filter.Eq(u => u.Login, login);
            Admins.ReplaceOne(filter, userFromBd);
            return userFromBd;
        }

    }
}
