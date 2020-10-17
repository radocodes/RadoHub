using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RadoHub.Services.Contracts;
using RadoHub.ViewModels.Lifestyle.Tools;

namespace RadoHub.WebApp.Areas.LifeStyle.Controllers
{
    public class ToolsController: LifeStyleControllerBase
    {
        private readonly ICloudinaryService cloudinaryService;

        public ToolsController(ICloudinaryService cloudinaryService)
        {
            this.cloudinaryService = cloudinaryService;
        }

        public IActionResult RakiaCalculator()
        {
            const string headerImageFileName = "jim-harris-zDlusnb3G3Q-unsplash_czwkgt";

            var viewModel = new RakiaCalculatorViewModel()
            {
                Cloudinary = this.cloudinaryService.GetCloudinaryInstance()
            };

            ViewData["HeaderImageFileName"] = headerImageFileName;

            return View(viewModel);
        }
    }
}