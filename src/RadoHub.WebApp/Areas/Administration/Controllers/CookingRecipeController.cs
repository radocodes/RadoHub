using Microsoft.AspNetCore.Mvc;
using RadoHub.Services.Contracts;
using RadoHub.ViewModels.CookingRecipes;

namespace RadoHub.WebApp.Areas.Administration.Controllers
{
    public class CookingRecipeController : AdministrationControllerBase
    {
        private readonly ICookingRecipeService cookingRecipeService;
        public CookingRecipeController(ICookingRecipeService cookingRecipeService)
        {
            this.cookingRecipeService = cookingRecipeService;
        }
        public IActionResult Index()
        {
            var viewModel = new AdminGetAllRecipesViewModel();

            viewModel.AllCookingRecipes = this.cookingRecipeService.GetAllCookingRecipes();

            return View(viewModel);
        }
    }
}