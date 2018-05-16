using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TaskBoard.Infrastucture;

namespace TaskBoard.Data
{
    public class FileRepository : IFileRepository
    {
        private const string FileStorage = "files";
        private const string ImageStorage = "images";

        private readonly UniqueNameService _uniqueNameService;

        public FileRepository(UniqueNameService uniqueNameService)
        {
            _uniqueNameService = uniqueNameService;
        }
       
        private async Task Save(string filePath, IFormFile file)
        {
            var path = Path.Combine(
                Directory.GetCurrentDirectory(), filePath);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        public async Task<string> SaveFile(IFormFile file)
        {
            return await SaveFileInStorage(FileStorage, file);
        }

        public async Task<string> SaveImage(IFormFile image)
        {
            return await SaveFileInStorage(ImageStorage, image);
        }

        public async Task<byte[]> GetFile(string fileName)
        {
            byte[] file;
            using (var fileStream = File.Open(fileName, FileMode.Open))
            {
                file = new byte[fileStream.Length];
                await fileStream.ReadAsync(file, 0, (int) fileStream.Length);
            }

            return file;
        }

        public async Task DeleteFile(string identifictor)
        {
            using (FileStream stream = new FileStream(
                identifictor, 
                FileMode.Truncate, 
                FileAccess.Write, 
                FileShare.Delete,
                4096, true))
            {
                await stream.FlushAsync();
                File.Delete(identifictor);
            }
        }

        private async Task<string> SaveFileInStorage(string storage, IFormFile file)
        {
            if (!Directory.Exists(storage))
            {
                Directory.CreateDirectory(storage);
            }

            var nameInStorage = _uniqueNameService.GetUniqueKey(file.FileName);
            var wayToFile = Path.Combine(storage, nameInStorage);
            await Save(wayToFile, file);
            return wayToFile;
        }
    }
}
