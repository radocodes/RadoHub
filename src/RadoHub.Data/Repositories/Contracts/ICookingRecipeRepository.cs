using RadoHub.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RadoHub.Data.Repositories.Contracts
{
    public interface ICookingRecipeRepository
    {
        IEnumerable<CookingRecipe> GetAllCookingRecipes();

        IEnumerable<CookingRecipe> GetCookingRecipesByHashtag(string hashtag);

        CookingRecipe GetCookingRecipeById(int cookingRecipeId);

        Task<int> CreateCookingRecipeAsync(CookingRecipe cookingRecipe);

        Task UpdateCookingRecipeAsync(CookingRecipe updatingModel);

        Task DeleteAsync(CookingRecipe cookingRecipe);
    }
}
