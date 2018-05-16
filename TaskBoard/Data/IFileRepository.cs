using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskBoard.Data
{
    public interface IFileRepository
    {
        Task<string> SaveFile(IFormFile file);
        Task<string> SaveImage(IFormFile image);
        Task<byte[]> GetFile(string identifictor);
        Task DeleteFile(string identifictor);
    }
}
