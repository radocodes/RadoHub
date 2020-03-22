using RadoHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RadoHub.ViewModels.CookingRecipes
{
    public class CookingRecipeViewModel : BaseModel<int>
    {
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public TimeSpan ExecutingTime { get; set; }

        public ICollection<string> Products { get; set; }

        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedAt { get; set; }

        public string CoverImageFileName { get; set; }

        public ICollection<string> ImagesFileNames { get; set; }

        public ICollection<string> Hashtags { get; set; }
    }
}
