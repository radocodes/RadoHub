using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RadoHub.Services.Contracts
{
    public interface IFileService
    {
        public void CreateDirectory(string fullPath);

        public void DeleteDirectory(string fullPath);

        public Task SaveImageFile(string fullPath, IFormFile imageFile);

        public Task UpdateImageFile(string fullPath, IFormFile imageFile);

        public void DeleteImageFile(string fullPath);
    }
}
