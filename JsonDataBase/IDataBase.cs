using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace SerializableDataBase
{
    public interface IDataBase
    {
        IDataBase Save(User user);
        List<User> GetUsers();
        bool CheckUser(User user);

        bool CheckUser(string login, string password);
        User GetUser(string email);
    }
}
