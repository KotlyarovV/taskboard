using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Libuv.Internal.Networking;
using TaskBoard.Models;
using TaskBoard.Models.User;

namespace TaskBoard.Data
{
    public class UserRepository : IUserRepository
    {
        private List<UserModel> registrations = new List<UserModel>();
        public IUserRepository Save(UserModel registration)
        {
            registrations.Add(registration);
            return this;
        }

        public bool CheckRegistration(LoginModel login)
        {
            return registrations.Any(registration => registration.Login == login.Username && registration.Password == login.Password);
        }

        public bool ExistenceVerification(string login)
        {
            return registrations.Any(registration => registration.Login == login);
        }

        public UserModel GetUser(string login)
        {
            return registrations.FirstOrDefault(user => user.Login == login);
        }

        public IUserRepository Update(string login, UserModel chacnges)
        {
            var user = GetUser(login);
            registrations.Remove(user);

            var type = chacnges.GetType();
            foreach(var propertyInfo in type.GetProperties())
            {
                var newUserProperty = propertyInfo.GetValue(chacnges);
                if (newUserProperty != null)
                {
                    propertyInfo.SetValue(user, newUserProperty);
                }
            }
            registrations.Add(user);
            return this;
        }

        public IEnumerable<UserModel> GetUsers()
        {
            return registrations;
        }
    }
}
