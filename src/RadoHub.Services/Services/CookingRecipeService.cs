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
                Content = model.Content,
                CreationDate = DateTime.UtcNow,
                LastModifiedAt = DateTime.UtcNow,
                CreatorId = creatorId,
            };

            if (model.Products != null)
            {
                cookingRecipe.Products = model.Products.Split((new[] { ',' }), StringSplitOptions.RemoveEmptyEntries).ToHashSet();
            }

            if (model.Hashtags != null)
            {
                cookingRecipe.Hashtags = model.Hashtags.Split((new[] { ',' }), StringSplitOptions.RemoveEmptyEntries).ToHashSet();
            }

            var newCookingRecipeId = this.cookingRecipeRepo
                .CreateCookingRecipeAsync(cookingRecipe)
                .GetAwaiter().GetResult();

            var sb = new StringBuilder();
            sb.Append(CookingRecipeConstants.StageImageFolderPath);
            sb.Append(CookingRecipeConstants.CookingRecipesImageFolderName);
            sb.Append(newCookingRecipeId);

            var currRecipeAllimagesPath = sb.ToString();

            sb.Append(CookingRecipeConstants.CoverImagefolderName);

            var currCoverImagePath = sb.ToString();

            this.fileService.CreateDirectory(currCoverImagePath);

            var coverImgFileName = $"{Guid.NewGuid().ToString()}{FileExtensions.ImageExtension}";

            if (model.CoverImage != null)
            {
                this.fileService.SaveImageFile($"{currCoverImagePath}{coverImgFileName}", model.CoverImage);
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

        public CookingRecipe GetCookingRecipeById(int cookingRecipeId)
        {
            return this.cookingRecipeRepo.GetCookingRecipeById(cookingRecipeId);
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
