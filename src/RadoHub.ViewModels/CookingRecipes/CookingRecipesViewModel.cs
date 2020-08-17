using CloudinaryDotNet;
using System.Collections.Generic;

namespace RadoHub.ViewModels.CookingRecipes
{
    public class CookingRecipesViewModel
    {
        public IEnumerable<CookingRecipeViewModel> Recipes { get; set; }

        public Cloudinary Cloudinary { get; set; }

    }
}
