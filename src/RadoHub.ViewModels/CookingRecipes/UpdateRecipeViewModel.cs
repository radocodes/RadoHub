using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RadoHub.ViewModels.CookingRecipes
{
    public class UpdateRecipeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Title")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Short description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Executing time")]
        public TimeSpan ExecutingTime { get; set; }

        public HashSet<string> ProductsToUpdate { get; set; }

        [Display(Name = "Products")]
        public string Products { get; set; }

        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "Cover image")]
        public IFormFile CoverImage { get; set; }

        [Display(Name = "Images")]
        public IEnumerable<IFormFile> Images { get; set; }

        public HashSet<string> HashtagsToUpdate { get; set; }

        [Display(Name = "Hashtags")]
        public string Hashtags { get; set; }

        public string CoverImageFileName { get; set; }

    }
}
