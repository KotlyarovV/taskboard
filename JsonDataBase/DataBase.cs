using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SerializableDataBase
{
    public class DataBase : IDataBase
    {

        private const string FileName = "students";

        public bool CheckUser(string login, string password)
        {
            return true;
        }
       
        public bool CheckUser(User user)
        {
            List<User> users = GetUsers();
            return users.Any(userFromCollection =>
                user.Email == userFromCollection.Email && user.PasswordMD5 == userFromCollection.PasswordMD5);
        }


        public List<User> GetUsers() => (!File.Exists(FileName)) ?  new List<User>() : (List<User>) ReadObjectFromFile();
        
        private void SaveUsers(List<User> users) => Save(FileName, users);
        

        private void Save(string fileName, Object objToSerialize)
        {
            FileStream fsStream = File.Open(fileName, FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fsStream, objToSerialize);
            fsStream.Close();
        }

        private Object ReadObjectFromFile()
        {
            Object objectToSerialize = null;
            FileStream fileStream = File.Open(FileName, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            objectToSerialize = binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return objectToSerialize;
        }

        public IDataBase Save(User user)
        {
            List<User> users = GetUsers();
            if (!users.Contains(user)) users.Add(user);
            SaveUsers(users);
            return this;
        }

        public User GetUser(string email)
        {
            List<User> users = GetUsers().FindAll(user => user.Email == email);
            return (users.Count == 0) ? null : users.FirstOrDefault();
        }
    }
}
