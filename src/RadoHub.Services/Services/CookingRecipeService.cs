using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
using Environment = RadoHub.Services.Constants.Environment;

namespace RadoHub.Services.Services
{
    public class CookingRecipeService : ICookingRecipeService
    {
        private readonly ICookingRecipeRepository cookingRecipeRepo;
        private readonly IUserAccountService userAccountService;
        private readonly IFileService fileService;
        private readonly IWebHostEnvironment env;

        public CookingRecipeService
            (ICookingRecipeRepository cookingRecipeRepository,
            IUserAccountService userAccountService,
            IFileService fileService,
            IWebHostEnvironment environment)
        {
            this.cookingRecipeRepo = cookingRecipeRepository;
            this.userAccountService = userAccountService;
            this.fileService = fileService;
            this.env = environment;
        }

        public void CreateCookingRecipe(string creatorId, CreateRecipeViewModel viewModel)
        {
            var cookingRecipe = new CookingRecipe()
            {
                Title = viewModel.Title,
                ShortDescription = viewModel.ShortDescription,
                ExecutingTime = viewModel.ExecutingTime,
                Products = viewModel.Products,
                Content = viewModel.Content,
                Hashtags = viewModel.Hashtags,
                CreationDate = DateTime.UtcNow,
                LastModifiedAt = DateTime.UtcNow,
                CreatorId = creatorId,
            };

            var newCookingRecipeId = this.cookingRecipeRepo
                .CreateCookingRecipeAsync(cookingRecipe)
                .GetAwaiter().GetResult();

            this.HandleImages(newCookingRecipeId, null, viewModel.CoverImage, viewModel.Images);
        }

        public async Task UpdateCookingRecipeAsync(string editorId, UpdateRecipeViewModel viewModel)
        {
            var updatingModel = this.cookingRecipeRepo.GetCookingRecipeById(viewModel.Id);

            updatingModel.Id = viewModel.Id;
            updatingModel.Title = viewModel.Title;
            updatingModel.ShortDescription = viewModel.ShortDescription;
            updatingModel.Products = viewModel.Products;
            updatingModel.Content = viewModel.Content;
            updatingModel.ExecutingTime = viewModel.ExecutingTime;
            updatingModel.Hashtags = viewModel.Hashtags;
            updatingModel.LastModifiedAt = DateTime.UtcNow;
            //TODO: improve editorsUsernames handling at all (db too)
            //EditorsUsernames = oldCookingRecipe.EditorsUsernames,



            await this.cookingRecipeRepo.UpdateCookingRecipeAsync(updatingModel);

            this.HandleImages(updatingModel.Id, viewModel.CoverImageFileName, viewModel.CoverImage, viewModel.Images);
        }

        public async Task DeleteAsync(int cookingRecipeId)
        {
            var cookingRecipe = this.cookingRecipeRepo.GetCookingRecipeById(cookingRecipeId);

            await this.cookingRecipeRepo.DeleteAsync(cookingRecipe);

            var sb = new StringBuilder();

            if (env.EnvironmentName == Environment.Development)
            {
                sb.Append(CookingRecipeConstants.StageImageFolderPath);
            }
            else if (env.EnvironmentName == Environment.Production)
            {
                sb.Append(CookingRecipeConstants.ProdImageFolderPath);
            }

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

        public UpdateRecipeViewModel GetRecipeToUpdate(int id)
        {
            var recipeToUpdate = this.GetCookingRecipeById(id);

            var updateModel = new UpdateRecipeViewModel()
            {
                Id = recipeToUpdate.Id,
                Title = recipeToUpdate.Title,
                ShortDescription = recipeToUpdate.ShortDescription,
                ExecutingTime = recipeToUpdate.ExecutingTime,
                Content = recipeToUpdate.Content,
                CoverImageFileName = recipeToUpdate.CoverImageFileName
            };

            if (recipeToUpdate.Products != null)
            {
                updateModel.ProductsToUpdate = recipeToUpdate.Products.Split((new[] { ',' }), StringSplitOptions.RemoveEmptyEntries).ToHashSet();
            }

            if (recipeToUpdate.Hashtags != null)
            {
                updateModel.HashtagsToUpdate = recipeToUpdate.Hashtags.Split((new[] { ',' }), StringSplitOptions.RemoveEmptyEntries).ToHashSet();
            }

            return updateModel;
        }

        private void HandleImages(int cookingRecipeId, string oldCoverImageFileName, IFormFile newCoverImage, IEnumerable<IFormFile> newImages)
        {
            string coverImgFileName = oldCoverImageFileName;

            var sb = new StringBuilder();

            if (env.EnvironmentName == Environment.Development)
            {
                sb.Append(CookingRecipeConstants.StageImageFolderPath);
            }

            else if (env.EnvironmentName == Environment.Production)
            {
                sb.Append(CookingRecipeConstants.ProdImageFolderPath);
            }

            sb.Append(CookingRecipeConstants.CookingRecipesImageFolderName);
            sb.Append(@$"{cookingRecipeId}\");

            var currRecipeAllimagesPath = sb.ToString();

            sb.Append(CookingRecipeConstants.CoverImagefolderName);

            var currCoverImagePath = sb.ToString();

            this.fileService.CreateDirectory(currCoverImagePath);

            if (newCoverImage != null)
            {
                if (string.IsNullOrEmpty(coverImgFileName))
                {
                    coverImgFileName = $"{Guid.NewGuid().ToString()}{FileExtensions.ImageExtension}";
                }

                this.fileService.SaveImageFile($"{currCoverImagePath}{coverImgFileName}", newCoverImage);
            }

            var updatingModel = this.cookingRecipeRepo.GetCookingRecipeById(cookingRecipeId);
            updatingModel.CoverImageFileName = coverImgFileName;
            this.cookingRecipeRepo.UpdateCookingRecipeAsync(updatingModel);

            // TODO: implementation for secondary images not completed 
            if (newImages != null && newImages.Count() > 0)
            {
                foreach (var image in newImages)
                {
                    var imageFileName = $"{Guid.NewGuid().ToString()}{FileExtensions.ImageExtension}";

                    this.fileService.SaveImageFile($"{currRecipeAllimagesPath}{imageFileName}", newCoverImage);
                }
            }
        }
    }
}
