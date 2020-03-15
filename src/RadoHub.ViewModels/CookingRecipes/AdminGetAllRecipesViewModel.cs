using System;
using System.Collections.Generic;
using System.Text;
using RadoHub.Data.Models;

namespace RadoHub.ViewModels.CookingRecipes
{
    public class AdminGetAllRecipesViewModel
    {
        public IEnumerable<Data.Models.CookingRecipe> AllCookingRecipes { get; set; }
    }
}
