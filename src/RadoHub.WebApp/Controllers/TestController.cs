using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using RadoHub.Services.Constants;
using System.IO;

namespace RadoHub.WebApp.Controllers
{
    [Authorize(Roles = UserRoles.AdminRole)]
    public class TestController : Controller
    {
        private readonly IWebHostEnvironment environment;

        public TestController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DownloadFileTest()
        {
            var wwwwrootPath = this.environment.WebRootPath;
            var filePath = $"{wwwwrootPath}/images/season-inspiration/April.jpg";

            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(filePath, out contentType);
            if (contentType == null)
            {
                contentType = "application/octet-stream";
            }
            
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return File(fileStream, contentType, "TestDownloadFileName.jpg");
        }
    }
}