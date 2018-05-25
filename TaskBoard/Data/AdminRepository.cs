using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TaskBoard.Models;

namespace TaskBoard.Data
{
    public class AdminRepository : IAdminRepository
    {
        private List<AdminModel> registrations = new List<AdminModel>();

        public AdminRepository()
        {
            using (StreamReader r = new StreamReader("admin.json"))
            {
                string json = r.ReadToEnd();
                AdminModel adminModel = JsonConvert.DeserializeObject<AdminModel>(json);
                registrations.Add(adminModel);
            }
        }

        public bool CheckRegistration(AdminModel model)
        {
            return registrations.Any(registration => registration.Login == model.Login && registration.Password == model.Password);
        }

        public bool ExistenceVerification(string login)
        {
            return registrations.Any(registration => registration.Login == login);
        }

        public AdminModel GetAdmin(string login)
        {
            return registrations.FirstOrDefault(user => user.Login == login);
        }

        public IEnumerable<AdminModel> GetAdmins()
        {
            return registrations;
        }

        public IAdminRepository Save(AdminModel registration)
        {
            registrations.Add(registration);
            return this;
        }

        public IAdminRepository Update(string login, AdminModel changes)
        {
            var user = GetAdmin(login);
            registrations.Remove(user);

            var type = changes.GetType();
            foreach (var propertyInfo in type.GetProperties())
            {
                var newUserProperty = propertyInfo.GetValue(changes);
                if (newUserProperty != null)
                {
                    propertyInfo.SetValue(user, newUserProperty);
                }
            }
            registrations.Add(user);
            return this;
        }
    }
}
