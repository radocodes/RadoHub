﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RadoHub.Data.Repositories.Contracts;
using RadoHub.Services.Contracts;
using RadoHub.ViewModels.CookingRecipes;
using RadoHub.ViewModels.Home;
using RadoHub.WebApp.Models;

namespace RadoHub.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICookingRecipeService cookingRecipeService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IInspirationRepository inspirationRepository;

        public HomeController(ILogger<HomeController> logger,
            ICookingRecipeService cookingRecipeService,
            ICloudinaryService cloudinaryService,
            IInspirationRepository inspirationRepository)
        {
            this._logger = logger;
            this.cookingRecipeService = cookingRecipeService;
            this.cloudinaryService = cloudinaryService;
            this.inspirationRepository = inspirationRepository;
        }

        public IActionResult Index()
        {
            var model = new HomeIndexViewModel();
            model.CookingRecipesModel = new CookingRecipesViewModel();
            model.CookingRecipesModel.Recipes = cookingRecipeService
                .GetAllRecipesAsPublic()
                .Where(recipe => recipe.CoverImageFileName != null).Take(3).ToList();

            model.Cloudinary = this.cloudinaryService.GetCloudinaryInstance();
            model.InspirationImageFileName = this.inspirationRepository.GetInspirationImage();

            return View(model);
        }

        public IActionResult About()
        {
            var model = new AboutViewModel();
            model.Cloudinary = this.cloudinaryService.GetCloudinaryInstance();

            return View(model);
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return Redirect(returnUrl);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
