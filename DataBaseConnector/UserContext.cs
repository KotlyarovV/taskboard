using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;


namespace DataBaseConnector
{
    public class UserContext
    {
        IMongoDatabase database; // база данных

        public UserContext()
        {
            var connectionString = "mongodb+srv://kotlvit:qwery@reports-qowm3.mongodb.net/test?retryWrites=true";
            var connection = new MongoUrlBuilder(connectionString);
            var client = new MongoClient(connectionString);
            database = client.GetDatabase(connection.DatabaseName);
        }

        private IMongoCollection<UserBD> Users => database.GetCollection<UserBD>("gipo");

        public async Task<IEnumerable<UserBD>> GetUsers()
        {
            var builder = new FilterDefinitionBuilder<UserBD>();
            var filter = builder.Empty; // фильтр для выборки всех документов
            return await Users.Find(filter).ToListAsync();
        }

        public bool CheckRegistration(string login, string password)
        {
            var user = Users
                .AsQueryable()
                .FirstOrDefault(u => u.Login == login && u.Password == password);
            return user != null;
        }

        public string SaveUser(UserBD user)
        {
            Users.InsertOne(user);
            return user.Id;
        }

        public bool ExistenceVerification(string login)
        {
            return Users.AsQueryable().Any(registration => registration.Login == login);
        }

        public UserBD GetUser(string login)
        {
            return Users
                .AsQueryable()
                .FirstOrDefault(u => u.Login == login);
        }

        public UserBD Update(string login, UserBD user)
        {
            var userFromBd = GetUser(login);

            var type = userFromBd.GetType();
            foreach (var propertyInfo in type.GetProperties())
            {
                var newUserProperty = propertyInfo.GetValue(user);
                if (newUserProperty != null)
                {
                    propertyInfo.SetValue(userFromBd, newUserProperty);
                }
            }

            var filter = Builders<UserBD>.Filter.Eq(u => u.Login, login);
            Users.ReplaceOne(filter, userFromBd);
            return userFromBd;
        }

       
    }
}
