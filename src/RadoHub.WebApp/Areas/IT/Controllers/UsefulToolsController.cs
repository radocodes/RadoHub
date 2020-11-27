using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using RadoHub.Services.Constants;
using RadoHub.Services.Contracts;
using RadoHub.ViewModels.IT.UsefulTools;
using System.IO;

namespace RadoHub.WebApp.Areas.IT.Controllers
{
    public class UsefulToolsController : ITControllerBase
    {
        private readonly ICloudinaryService cloudinaryService;
        private readonly IWebHostEnvironment environment;
        public UsefulToolsController(ICloudinaryService cloudinaryService, IWebHostEnvironment environment)
        {
            this.cloudinaryService = cloudinaryService;
            this.environment = environment;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Library()
        {
            LibraryViewModel viewModel = new LibraryViewModel()
            {
                Cloudinary = this.cloudinaryService.GetCloudinaryInstance()
            };

            ViewData["UnderContructionImageFileName"] = GlobalConstants.UnderConstructionImageFileName;

            return View(viewModel);
        }

        public IActionResult SvgSpriteSplitter()
        {
            return this.View();
        }

        public IActionResult DownloadSvgSpriteSplitter(string type)
        {
            var existingTypes = new string []{ "win64", "win86", "linux64" };

            foreach (var item in existingTypes)
            {
                if (item == type)
                {
                    var wwwwrootPath = this.environment.WebRootPath;
                    var filePath = $"{wwwwrootPath}/download/apps/svg-sprite-splitter/{type}.7z";

                    string contentType;
                    new FileExtensionContentTypeProvider().TryGetContentType(filePath, out contentType);
                    if (contentType == null)
                    {
                        contentType = "application/octet-stream";
                    }

                    var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    return File(fileStream, contentType, $"SVG_Sprite_Splitter_{type}.7z");
                }
            }

            return BadRequest();
        }
    }
}