using System;
using System.Collections.Generic;
using System.Text;

namespace RadoHub.ViewModels.CookingRecipes
{
    public class CookingRecipesViewModel
    {
        public IEnumerable<CookingRecipeViewModel> Recipes { get; set; }
    }
}
