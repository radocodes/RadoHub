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
            model.Cloudinary = this.cloudinaryService.GetCloudinaryInstance();

            return this.View(model);
        }

        public IActionResult RecipeDetails(int id)
        {
            var viewModel = this.cookingRecipeService.GetCookingRecipeByIdAsPublic(id);
            viewModel.Cloudinary = cloudinaryService.GetCloudinaryInstance();

            return this.View(viewModel);
        }
    }
}