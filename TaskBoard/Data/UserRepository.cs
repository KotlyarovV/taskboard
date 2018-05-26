using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBaseConnector;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Libuv.Internal.Networking;
using TaskBoard.Models;
using TaskBoard.Models.User;

namespace TaskBoard.Data
{
    public class UserRepository : IUserRepository
    {
        private UserContext _userContext;

        public UserRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public IUserRepository Save(UserModel registration)
        {
            _userContext.SaveUser(Transform(registration));
            return this;
        }

        public bool CheckRegistration(LoginModel login)
        {
            return _userContext.CheckRegistration(login.Username, login.Password);
        }

        public bool ExistenceVerification(string login)
        {
            return _userContext.ExistenceVerification(login);
        }

        public UserModel GetUser(string login)
        {
            var user = _userContext.GetUser(login);
            var userFromBd = Transform(user);
            return userFromBd;
        }

        public IUserRepository Update(string login, UserModel chacnges)
        {
            var user = _userContext.Update(login, Transform(chacnges));
            return this;
        }

        public IEnumerable<UserModel> GetUsers()
        {
            return _userContext.GetUsers().GetAwaiter().GetResult().Select(Transform);
        }


        public static UserBD Transform(UserModel entity)
        {
            return new UserBD()
            {
                Education = entity.Education,
                Email = entity.Email,
                Information = entity.Information,
                InterestedTheme = entity.InterestedTheme,
                Login = entity.Login,
                Name = entity.Name,
                PhotoLink = entity.PhotoLink,
                Password = entity.Password,
                Phone = entity.Phone,
                SecondName = entity.SecondName,
                WorksOrdered = entity.WorksOrdered,
                WorksPerformed = entity.WorksPerformed
            };
        }

        public static UserModel Transform(UserBD entity)
        {
            return new UserModel()
            {
                Education = entity.Education,
                Email = entity.Email,
                Information = entity.Information,
                InterestedTheme = entity.InterestedTheme,
                Login = entity.Login,
                Name = entity.Name,
                PhotoLink = entity.PhotoLink,
                Password = entity.Password,
                Phone = entity.Phone,
                SecondName = entity.SecondName,
                WorksOrdered = entity.WorksOrdered,
                WorksPerformed = entity.WorksPerformed
            };
        }
    }
}
