using CloudinaryDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RadoHub.ViewModels.CookingRecipes
{
    public class UpdateRecipeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Title")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Short description")]
        [StringLength(300)]
        public string ShortDescription { get; set; }

        [Display(Name = "Executing time")]
        public TimeSpan ExecutingTime { get; set; }

        public HashSet<string> ProductsToUpdate { get; set; }

        [Display(Name = "Products")]
        public string Products { get; set; }

        [Display(Name = "Content")]
        public string Content { get; set; }

        public HashSet<string> HashtagsToUpdate { get; set; }

        [Display(Name = "Hashtags")]
        public string Hashtags { get; set; }

        public string CoverImageFileName { get; set; }

        public Account CloudinaryAccount { get; set; }
    }
}
