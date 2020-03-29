using RadoHub.Data.Models;
using RadoHub.ViewModels.CookingRecipes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RadoHub.Services.Contracts
{
    public interface ICookingRecipeService
    {
        List<CookingRecipe> GetAllCookingRecipes();

        List<CookingRecipeViewModel> GetAllRecipesAsPublic();

        List<CookingRecipe> GetCookingRecipesByKeyWords(string userSearching);

        CookingRecipe GetCookingRecipeById(int cookingRecipeId);

        CookingRecipeViewModel GetCookingRecipeByIdAsPublic(int cookingRecipeId);

        void CreateCookingRecipe(string creatorId, CreateRecipeViewModel model);

        Task UpdateCookingRecipeAsync(CookingRecipe updatingModel);

        Task DeleteAsync(int cookingRecipeId);
    }
}
