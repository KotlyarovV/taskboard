using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TaskBoard.Infrastucture
{
    public class UniqueNameService
    {
        public string GetUniqueKey(string fileName) =>
            DateTime.Now.ToString().GetHashCode().ToString("x") +
            Guid.NewGuid() +
            fileName.GetHashCode() +
            Path.GetExtension(fileName);

    }
}
