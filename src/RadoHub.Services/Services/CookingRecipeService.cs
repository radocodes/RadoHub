using RadoHub.Data.Models;
using RadoHub.Data.Repositories.Contracts;
using RadoHub.Services.Contracts;
using RadoHub.ViewModels.CookingRecipe;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RadoHub.Services.Services
{
    public class CookingRecipeService : ICookingRecipeService
    {
        private readonly ICookingRecipeRepository cookingRecipeRepo;

        public CookingRecipeService(ICookingRecipeRepository cookingRecipeRepository)
        {
            this.cookingRecipeRepo = cookingRecipeRepository;
        }

        public Task CreateCookingRecipeAsync(CreateCookingRecipeViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int cookingRecipeId)
        {
            throw new NotImplementedException();
        }

        public List<CookingRecipe> GetAllCookingRecipes()
        {
            throw new NotImplementedException();
        }

        public CookingRecipe GetCookingRecipeById(int cookingRecipeId)
        {
            throw new NotImplementedException();
        }

        public List<CookingRecipe> GetCookingRecipesByKeyWords(string userSearching)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCookingRecipeAsync(CookingRecipe updatingModel)
        {
            throw new NotImplementedException();
        }
    }
}
