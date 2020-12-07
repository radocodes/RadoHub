using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RadoHub.Data.Repositories.Contracts;
using RadoHub.Services.Contracts;
using RadoHub.ViewModels.CookingRecipes;
using System.Collections.Generic;

namespace RadoHub.WebApp.Areas.LifeStyle.Controllers.Cooking
{
    public class CookingController : LifeStyleControllerBase
    {
        private readonly ICookingRecipeRepository cookingRecipeRepo;
        private readonly ICookingRecipeService cookingRecipeService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IMapper mapper;

        public CookingController(
            ICookingRecipeRepository cookingRecipeRepository,
            ICookingRecipeService cookingRecipeService,
            ICloudinaryService cloudinaryService,
            IMapper mapper)
        {
            this.cookingRecipeService = cookingRecipeService;
            this.cloudinaryService = cloudinaryService;
            this.cookingRecipeRepo = cookingRecipeRepository;
            this.mapper = mapper;

        }

        public IActionResult Index(string currentFilter, string searchString, int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var model = new CookingRecipesViewModel();

            if (!string.IsNullOrEmpty(searchString))
            {
                model.Recipes = mapper.Map<List<CookingRecipeViewModel>>(this.cookingRecipeRepo.GetAllCookingRecipesByKeyword(searchString));
            }
            else
            {
                model.Recipes = mapper.Map<List<CookingRecipeViewModel>>(this.cookingRecipeRepo.GetAllCookingRecipes());
            }
            
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