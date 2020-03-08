using RadoHub.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RadoHub.Data.Repositories.Contracts
{
    public interface ICookingRecipeRepository
    {
        ICollection<CookingRecipe> GetAllCookingRecipes();

        ICollection<CookingRecipe> GetCookingRecipesByHashTags();

        CookingRecipe GetCookingRecipeById(int CookingRecipeId);

        CookingRecipe CreateCookingRecipe(CookingRecipe cookingRecipe);

        void UpdateCookingRecipe(CookingRecipe UpdatingModel);

        void Delete(CookingRecipe cookingRecipe);
    }
}
