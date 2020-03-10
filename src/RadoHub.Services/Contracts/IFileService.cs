using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RadoHub.Services.Contracts
{
    public interface IFileService
    {
        Task SaveImageFile(string fullPath, IFormFile imageFile);

        public void DeleteImageFile(string fullPath);
    }
}
