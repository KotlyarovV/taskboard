using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBaseConnector;
using TaskBoard.Models;

namespace TaskBoard.Data
{
    public interface IAdminRepository
    {
        IAdminRepository Save(AdminModel registration);
        bool CheckRegistration(AdminModel login);
        bool ExistenceVerification(string login);
        AdminModel GetAdmin(string login);
        IAdminRepository Update(string login, AdminModel changes);
        IEnumerable<AdminModel> GetAdmins();
    }
}
