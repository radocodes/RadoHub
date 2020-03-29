using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RadoHub.Services.Contracts;
using RadoHub.ViewModels.CookingRecipes;

namespace RadoHub.WebApp.Areas.LifeStyle.Controllers.Cooking
{
    public class CookingController : LifeStyleControllerBase
    {
        private readonly ICookingRecipeService cookingRecipeService;

        public CookingController(ICookingRecipeService cookingRecipeService)
        {
            this.cookingRecipeService = cookingRecipeService;
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

            return this.View(viewModel);
        }
    }
}