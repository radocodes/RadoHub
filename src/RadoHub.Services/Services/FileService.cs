using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RadoHub.Services.Services
{
    public class FileService
    {
        public async Task SaveImageFile(string fullPath, IFormFile imageFile)
        {
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            };
        }
    }
}
