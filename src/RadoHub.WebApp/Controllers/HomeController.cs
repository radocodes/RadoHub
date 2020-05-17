using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        public HomeController(ILogger<HomeController> logger, ICookingRecipeService cookingRecipeService)
        {
            this._logger = logger;
            this.cookingRecipeService = cookingRecipeService;
        }

        public IActionResult Index()
        {
            var model = new HomeIndexViewModel();
            model.CookingRecipesModel = new CookingRecipesViewModel();
            model.CookingRecipesModel.Recipes = cookingRecipeService
                .GetAllRecipesAsPublic()
                .Where(recipe => recipe.CoverImageFileName != null).Take(3).ToList();

            return View(model);
        }

        public IActionResult About()
        {
            return View();
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
