using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskBoard.Models;
using TaskBoard.Models.User;

namespace TaskBoard.Data
{
    public interface IUserRepository
    {
        IUserRepository Save(UserModel registration);
        bool CheckRegistration(LoginModel login);
        bool ExistenceVerification(string login);
        UserModel GetUser(string login);
        IUserRepository Update(string login, UserModel chacnges);
    }
}
