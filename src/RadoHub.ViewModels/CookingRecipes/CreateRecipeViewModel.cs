﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RadoHub.ViewModels.CookingRecipes
{
    public class CreateRecipeViewModel
    {
        [Display(Name = "Title")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Short description")]
        [StringLength(300)]
        public string ShortDescription { get; set; }

        [Display(Name = "Executing time")]
        public TimeSpan ExecutingTime { get; set; }

        [Display(Name = "Products")]
        [Required]
        public string Products { get; set; }

        [Display(Name = "Content")]
        [Required]
        public string Content { get; set; }

        [Display(Name = "Cover image")]
        public IFormFile CoverImage { get; set; }

        [Display(Name = "Images")]
        public IEnumerable<IFormFile> Images { get; set; }

        [Display(Name = "Hashtags")]
        public string Hashtags { get; set; }
    }
}
