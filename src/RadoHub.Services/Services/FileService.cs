using Microsoft.AspNetCore.Http;
using RadoHub.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RadoHub.Services.Services
{
    public class FileService : IFileService
    {
        public void CreateDirectory(string fullPath)
        {
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
            //else
            //{
            //    var exeptionMessage = "Operation Failed! Directory you tried to create already exist";
            //    throw new Exception(exeptionMessage);
            //}
        }

        public void DeleteDirectory(string fullPath)
        {
            if (Directory.Exists(fullPath))
            {
                Directory.Delete(fullPath, true);
            }
            else
            {
                var exeptionMessage = "Operation Failed! Directory you tried to delete is not found";
                throw new DirectoryNotFoundException(exeptionMessage);
            }
        }

        public async Task SaveImageFile(string fullPath, IFormFile imageFile)
        {
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            };
        }

        public void DeleteImageFile(string fullPath)
        {
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            else
            {
                var exeptionMessage = "Operation Failed! The file you tried to delete is not found";
                throw new FileNotFoundException(exeptionMessage, fullPath);
            }
        }

        public async Task UpdateImageFile(string fullPath, IFormFile imageFile)
        {
            await this.SaveImageFile(fullPath, imageFile);
        }
    }
}
