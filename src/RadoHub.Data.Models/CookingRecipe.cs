using System;

namespace RadoHub.Data.Models
{
    public class CookingRecipe : BaseModel<int>
    {
        public CookingRecipe()
        {

        }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public TimeSpan ExecutingTime { get; set; }

        public string Products{ get; set; }

        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedAt { get; set; }

        public string CreatorId { get; set; }

        public string EditorsUsernames { get; set; }

        public string CoverImageFileName { get; set; }

        public string ImagesFileNames { get; set; }

        public string Hashtags { get; set; }

    }
}
