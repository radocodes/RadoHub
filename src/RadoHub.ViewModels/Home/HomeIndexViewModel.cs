using CloudinaryDotNet;
using RadoHub.ViewModels.CookingRecipes;

namespace RadoHub.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public CookingRecipesViewModel CookingRecipesModel { get; set; }

        public Cloudinary Cloudinary { get; set; }

        public string InspirationImageFileName { get; set; }

    }
}
