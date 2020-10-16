using Microsoft.AspNetCore.Mvc;
using RadoHub.Services.Constants;
using RadoHub.Services.Contracts;
using RadoHub.ViewModels.IT.UsefulTools;

namespace RadoHub.WebApp.Areas.IT.Controllers
{
    public class UsefulToolsController : ITControllerBase
    {
        private readonly ICloudinaryService cloudinaryService;

        public UsefulToolsController(ICloudinaryService cloudinaryService)
        {
            this.cloudinaryService = cloudinaryService;
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
    }
}