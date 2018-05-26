using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataBaseConnector;
using TaskBoard.Models;

namespace TaskBoard.Data
{
    public class AdminRepository : IAdminRepository
    {
        private AdminContext _adminContext;
        public AdminRepository(AdminContext adminContext)
        {
            _adminContext = adminContext;
            using (StreamReader r = new StreamReader("admin.json"))
            {
                var json = r.ReadToEnd();
                var adminModel = JsonConvert.DeserializeObject<AdminModel>(json);
                Save(adminModel);
            }
        }

        public IAdminRepository Save(AdminModel registration)
        {
            _adminContext.Save(registration);
            return this;
        }

        public bool CheckRegistration(AdminModel login)
        {
            return _adminContext.CheckRegistration(login.Login, login.Password);
        }

        public bool ExistenceVerification(string login)
        {
            return _adminContext.ExistenceVerification(login);
        }

        public AdminModel GetAdmin(string login)
        {
            return _adminContext.GetAdmin(login);
        }

        public IAdminRepository Update(string login, AdminModel changes)
        {
            _adminContext.Update(login, changes);
            return this;
        }

        public IEnumerable<AdminModel> GetAdmins()
        {
            return _adminContext.GetAdmins().GetAwaiter().GetResult();
        }
    }
}
