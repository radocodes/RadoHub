using RadoHub.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RadoHub.Data.Repositories.Contracts
{
    public interface ICookingRecipeRepository
    {
        IEnumerable<CookingRecipe> GetAllCookingRecipes();

        IEnumerable<CookingRecipe> GetCookingRecipesByHashtag(string hashtag);

        CookingRecipe GetCookingRecipeById(int cookingRecipeId);

        Task CreateCookingRecipeAsync(CookingRecipe cookingRecipe);

        Task UpdateCookingRecipeAsync(CookingRecipe updatingModel);

        Task DeleteAsync(CookingRecipe cookingRecipe);
    }
}
