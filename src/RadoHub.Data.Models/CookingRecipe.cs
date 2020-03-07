using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped]
        public ICollection<string> Products{ get; set; }

        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedAt { get; set; }

        public string CreatorId { get; set; }

        [NotMapped]
        public Queue<string> EditorsUsernames { get; set; }

        public string CoverImageFileName { get; set; }

        [NotMapped]
        public ICollection<string> ImagesFileNames { get; set; }

        [NotMapped]
        public ICollection<string> Hashtags { get; set; }

    }
}
