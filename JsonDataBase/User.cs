using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Build.Framework;
using TaskBoard;

namespace SerializableDataBase
{
    [Serializable]
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordMD5 { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Education { get; set; }
        public Theme InterestedTheme { get; set; }
        public string Information { get; set; }
        public IFormFile Photo { get; set; }
    }
}
