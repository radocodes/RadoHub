using RadoHub.Data.Models;
using RadoHub.Data.Repositories.Contracts;
using RadoHub.Services.Constants;
using RadoHub.Services.Contracts;
using RadoHub.ViewModels.CookingRecipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadoHub.Services.Services
{
    public class CookingRecipeService : ICookingRecipeService
    {
        private readonly ICookingRecipeRepository cookingRecipeRepo;
        private readonly IUserAccountService userAccountService;
        private readonly IFileService fileService;

        public CookingRecipeService
            (ICookingRecipeRepository cookingRecipeRepository,
            IUserAccountService userAccountService,
            IFileService fileService)
        {
            this.cookingRecipeRepo = cookingRecipeRepository;
            this.userAccountService = userAccountService;
            this.fileService = fileService;
        }

        public void CreateCookingRecipe(string creatorId, CreateRecipeViewModel model)
        {
            var cookingRecipe = new CookingRecipe()
            {
                Title = model.Title,
                ShortDescription = model.ShortDescription,
                ExecutingTime = model.ExecutingTime,
                Products = model.Products,
                Content = model.Content,
                Hashtags = model.Hashtags,
                CreationDate = DateTime.UtcNow,
                LastModifiedAt = DateTime.UtcNow,
                CreatorId = creatorId,
            };

            var newCookingRecipeId = this.cookingRecipeRepo
                .CreateCookingRecipeAsync(cookingRecipe)
                .GetAwaiter().GetResult();

            var sb = new StringBuilder();
            sb.Append(CookingRecipeConstants.StageImageFolderPath);
            sb.Append(CookingRecipeConstants.CookingRecipesImageFolderName);
            sb.Append(@$"{newCookingRecipeId}\");

            var currRecipeAllimagesPath = sb.ToString();

            sb.Append(CookingRecipeConstants.CoverImagefolderName);

            var currCoverImagePath = sb.ToString();

            this.fileService.CreateDirectory(currCoverImagePath);

            if (model.CoverImage != null)
            {
                var coverImgFileName = $"{Guid.NewGuid().ToString()}{FileExtensions.ImageExtension}";

                this.fileService.SaveImageFile($"{currCoverImagePath}{coverImgFileName}", model.CoverImage);

                var updatingModel = this.GetCookingRecipeById(newCookingRecipeId);
                updatingModel.CoverImageFileName = coverImgFileName;

                this.cookingRecipeRepo.UpdateCookingRecipeAsync(updatingModel);
            }

            if (model.Images != null && model.Images.Count() > 0)
            {
                foreach (var image in model.Images)
                {
                    var imageFileName = $"{Guid.NewGuid().ToString()}{FileExtensions.ImageExtension}";

                    this.fileService.SaveImageFile($"{currRecipeAllimagesPath}{imageFileName}", model.CoverImage);
                }
            }
        }

        public async Task DeleteAsync(int cookingRecipeId)
        {
            var cookingRecipe = this.cookingRecipeRepo.GetCookingRecipeById(cookingRecipeId);

            await this.cookingRecipeRepo.DeleteAsync(cookingRecipe);

            var sb = new StringBuilder();
            sb.Append(CookingRecipeConstants.StageImageFolderPath);
            sb.Append(CookingRecipeConstants.CookingRecipesImageFolderName);
            sb.Append(cookingRecipeId);

            var currRecipeimagePath = sb.ToString();
            this.fileService.DeleteDirectory(currRecipeimagePath);
        }

        public List<CookingRecipe> GetAllCookingRecipes()
        {
            return this.cookingRecipeRepo.GetAllCookingRecipes().ToList();
        }

        public List<CookingRecipeViewModel> GetAllRecipesAsPublic()
        {
            var allRecipes = this.GetAllCookingRecipes();
            var publicRecipes = new List<CookingRecipeViewModel>();

            foreach (var recipe in allRecipes)
            {
                var recipeModel = new CookingRecipeViewModel()
                {
                    Id = recipe.Id,
                    Title = recipe.Title,
                    CreationDate = recipe.CreationDate,
                    LastModifiedAt = recipe.LastModifiedAt,
                    CoverImageFileName = recipe.CoverImageFileName,
                    ShortDescription = recipe.ShortDescription
                };

                publicRecipes.Add(recipeModel);
            }

            return publicRecipes;
        }

        public CookingRecipe GetCookingRecipeById(int cookingRecipeId)
        {
            return this.cookingRecipeRepo.GetCookingRecipeById(cookingRecipeId);
        }

        public CookingRecipeViewModel GetCookingRecipeByIdAsPublic(int cookingRecipeId)
        {
            var recipe = this.cookingRecipeRepo.GetCookingRecipeById(cookingRecipeId);

            var recipeModel = new CookingRecipeViewModel()
            {
                Id = recipe.Id,
                Title = recipe.Title,
                ShortDescription = recipe.ShortDescription,
                ExecutingTime = recipe.ExecutingTime,
                Content = recipe.Content,
                CreationDate = recipe.CreationDate,
                LastModifiedAt = recipe.LastModifiedAt,
                CoverImageFileName = recipe.CoverImageFileName
            };

            if (recipe.Products != null)
            {
                recipeModel.Products = recipe.Products.Split((new[] { ',' }), StringSplitOptions.RemoveEmptyEntries).ToHashSet();
            }

            if (recipe.Hashtags != null)
            {
                recipeModel.Hashtags = recipe.Hashtags.Split((new[] { ',' }), StringSplitOptions.RemoveEmptyEntries).ToHashSet();
            }

            if (recipe.ImagesFileNames != null)
            {
                recipeModel.ImagesFileNames = recipe.ImagesFileNames.Split((new[] { ',' }), StringSplitOptions.RemoveEmptyEntries).ToHashSet();
            }

            return recipeModel;
        }

        public List<CookingRecipe> GetCookingRecipesByKeyWords(string userSearching)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCookingRecipeAsync(CookingRecipe updatingModel)
        {
            throw new NotImplementedException();
        }
    }
}
