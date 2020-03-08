using RadoHub.Data.Models;
using RadoHub.ViewModels.CookingRecipe;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RadoHub.Services.Contracts
{
    public interface ICookingRecipeService
    {
        List<CookingRecipe> GetAllCookingRecipes();

        List<CookingRecipe> GetCookingRecipesByKeyWords(string userSearching);

        CookingRecipe GetCookingRecipeById(int cookingRecipeId);

        Task CreateCookingRecipeAsync(CreateCookingRecipeViewModel model);

        Task UpdateCookingRecipeAsync(CookingRecipe updatingModel);

        Task DeleteAsync(int cookingRecipeId);
    }
}
