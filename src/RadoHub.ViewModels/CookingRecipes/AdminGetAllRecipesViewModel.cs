using System.Collections.Generic;

namespace RadoHub.ViewModels.CookingRecipes
{
    public class AdminGetAllRecipesViewModel
    {
        public IEnumerable<Data.Models.CookingRecipe> AllCookingRecipes { get; set; }
    }
}
