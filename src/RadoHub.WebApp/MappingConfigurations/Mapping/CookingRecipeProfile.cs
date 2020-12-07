using AutoMapper;
using RadoHub.Data.Models;
using RadoHub.ViewModels.CookingRecipes;

namespace RadoHub.WebApp.MappingConfigurations.Mapping
{
    public class CookingRecipeProfile : Profile
    {
        public CookingRecipeProfile()
        {
            this.CreateMap<CookingRecipe, CookingRecipeViewModel>();
        }
    }
}
