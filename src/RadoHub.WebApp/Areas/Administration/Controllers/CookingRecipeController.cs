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
            if (TempData["statusMessage"] != null)
            {
                ViewData["statusMessage"] = TempData["statusMessage"];
            }

            var viewModel = new AdminGetAllRecipesViewModel();

            viewModel.AllCookingRecipes = this.cookingRecipeService.GetAllCookingRecipes();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DeleteCookingRecipe(int id)
        {
            try
            {
                this.cookingRecipeService.DeleteAsync(id).GetAwaiter().GetResult();

                TempData["statusMessage"] = "Cooking recipe was removed successfully!";
                return RedirectToAction("Index", "CookingRecipe");
            }
            catch (System.Exception exeption)
            {
                TempData["statusMessage"] = $"Action Failed! | {exeption.Message}";
                return RedirectToAction("Index", "CookingRecipe");
            }
            

        }
    }
}