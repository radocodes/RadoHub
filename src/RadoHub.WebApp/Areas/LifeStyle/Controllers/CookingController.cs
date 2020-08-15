using Microsoft.AspNetCore.Mvc;
using RadoHub.Services.Contracts;
using RadoHub.ViewModels.CookingRecipes;

namespace RadoHub.WebApp.Areas.LifeStyle.Controllers.Cooking
{
    public class CookingController : LifeStyleControllerBase
    {
        private readonly ICookingRecipeService cookingRecipeService;
        private readonly ICloudinaryService cloudinaryService;

        public CookingController(ICookingRecipeService cookingRecipeService, ICloudinaryService cloudinaryService)
        {
            this.cookingRecipeService = cookingRecipeService;
            this.cloudinaryService = cloudinaryService;
        }

        public IActionResult Index()
        {
            var model = new CookingRecipesViewModel();

            model.Recipes = this.cookingRecipeService.GetAllRecipesAsPublic();

            return this.View(model);
        }

        public IActionResult RecipeDetails(int id)
        {
            var viewModel = this.cookingRecipeService.GetCookingRecipeByIdAsPublic(id);
            viewModel.CloudinaryAccount = cloudinaryService.CloudinaryAccount();

            return this.View(viewModel);
        }
    }
}