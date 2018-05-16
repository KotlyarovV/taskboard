using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskBoard.Data;

namespace TaskBoard.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileRepository _fileRepository;
        public FileController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        [HttpGet("[controller]/{*file}")]
        [HttpPost("[controller]/{*file}")]
        public async Task<IActionResult> File(string file)
        {
            var answer = await _fileRepository.GetFile(file);
            return File(answer, "application/octet-stream");
        }
    }
}