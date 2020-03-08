using RadoHub.Data.Models;
using RadoHub.Data.Repositories.Contracts;
using RadoHub.WebApp.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RadoHub.Data.Repositories.Implementation
{
    public class CookingRecipeRepository : ICookingRecipeRepository
    {
        private readonly RadoHubDbContext DbContext;

        public CookingRecipeRepository(RadoHubDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public async Task<int> CreateCookingRecipeAsync(CookingRecipe cookingRecipe)
        {
            await this.DbContext.CookingRecipes.AddAsync(cookingRecipe);
            await this.DbContext.SaveChangesAsync();

            //possible incorect id
            return cookingRecipe.Id;
        }

        public async Task DeleteAsync(CookingRecipe cookingRecipe)
        {
            this.DbContext.CookingRecipes.Remove(cookingRecipe);
            await this.DbContext.SaveChangesAsync();

        }

        public IEnumerable<CookingRecipe> GetAllCookingRecipes()
        {
            return this.DbContext.CookingRecipes;
        }

        public CookingRecipe GetCookingRecipeById(int cookingRecipeId)
        {
            return this.DbContext.CookingRecipes.FirstOrDefault(recipe => recipe.Id == cookingRecipeId);
        }

        public IEnumerable<CookingRecipe> GetCookingRecipesByHashtag(string hashtag)
        {
            return this.DbContext.CookingRecipes.Where(recipe => recipe.Hashtags.Contains(hashtag));
        }

        public async Task UpdateCookingRecipeAsync(CookingRecipe updatingModel)
        {
            this.DbContext.CookingRecipes.Update(updatingModel);
            await this.DbContext.SaveChangesAsync();
        }
    }
}
