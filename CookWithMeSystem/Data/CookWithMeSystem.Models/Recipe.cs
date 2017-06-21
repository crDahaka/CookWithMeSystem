namespace CookWithMeSystem.Models
{
    using System.Collections.Generic;

    public class Recipe : BaseEntity
    {
        public string Title { get; set; }
        
        public string Description { get; set; }

        public string PublisherID { get; set; }

        public virtual User Publisher { get; set; }

        public int? ImageID { get; set; }

        public virtual Image Image { get; set; }

        public int? CategoryID { get; set; }

        public virtual Category Category { get; set; }

        public bool IsPrivate { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }

        public virtual ICollection<Step> Steps { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public Recipe()
        {
            this.Ingredients = new HashSet<Ingredient>();
            this.Steps = new HashSet<Step>();
            this.Comments = new HashSet<Comment>();
            this.Votes = new HashSet<Vote>();
        }
        
    }
}
