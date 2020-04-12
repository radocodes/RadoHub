using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RadoHub.Data.Models;
using RadoHub.Services.Contracts;
using RadoHub.ViewModels.CookingRecipes;

namespace RadoHub.WebApp.Areas.Administration.Controllers
{
    public class CookingRecipeController : AdministrationControllerBase
    {
        private readonly ICookingRecipeService cookingRecipeService;
        private readonly UserManager<RadoHubUser> userManager;

        public CookingRecipeController(ICookingRecipeService cookingRecipeService, UserManager<RadoHubUser> userManager)
        {
            this.cookingRecipeService = cookingRecipeService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            if (TempData["statusMessage"] != null)
            {
                ViewData["statusMessage"] = TempData["statusMessage"];
            }

            var viewModel = new AdminGetAllRecipesViewModel();

            viewModel.AllCookingRecipes = this.cookingRecipeService.GetAllCookingRecipes();

            return this.View(viewModel);
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

        public IActionResult CreateCookingRecipe()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateCookingRecipe(CreateRecipeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                var creatorId = this.userManager
                    .GetUserAsync(HttpContext.User)
                    .GetAwaiter().GetResult()
                    .Id;

                this.cookingRecipeService.CreateCookingRecipe(creatorId, model);

                TempData["statusMessage"] = "Cooking recipe was created successfully!";
                return RedirectToAction("Index", "CookingRecipe");
            }
            catch (System.Exception exeption)
            {
                ModelState.AddModelError("Action Failed!", exeption.Message);

                return this.View(model);

                //another approach
                //TempData["statusMessage"] = $"Action Failed! | {exeption.Message}";
                //return RedirectToAction("Index", "CookingRecipe");
            }
        }

        public IActionResult EditCookingRecipe(int Id)
        {
            //TODO: impelementation 
            
            return this.View();
        }

        [HttpPost]
        public IActionResult EditCookingRecipe(EditRecipeViewModel model)
        {
            //TODO: impelementation

            return this.View();
        }
    }
}