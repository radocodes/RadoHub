using System;
using System.Collections.Generic;
using System.Text;

namespace RadoHub.Data.Models
{
    public class CookingRecipe : BaseModel<int>
    {
        public CookingRecipe()
        {
            this.Products = new HashSet<string>();
            this.EditorsUsernames = new Queue<string>();
            this.ImagesFileNames = new HashSet<string>();
            this.Hashtags = new HashSet<string>();
        }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public TimeSpan ExecutingTime { get; set; }

        public ICollection<string> Products{ get; set; }

        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedAt { get; set; }

        public string CreatorId { get; set; }

        public Queue<string> EditorsUsernames { get; set; }

        public string CoverImageFileName { get; set; }

        public ICollection<string> ImagesFileNames { get; set; }

        public ICollection<string> Hashtags { get; set; }

    }
}
