using RadoHub.Data.Models;
using RadoHub.ViewModels.CookingRecipes;
using System.Collections.Generic;
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

        public UpdateRecipeViewModel GetRecipeToUpdate(int id);

        void CreateCookingRecipe(string creatorId, CreateRecipeViewModel model);

        Task UpdateCookingRecipeAsync(string editorId, UpdateRecipeViewModel model);

        Task DeleteAsync(int cookingRecipeId);
    }
}
