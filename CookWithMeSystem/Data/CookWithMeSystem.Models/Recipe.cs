namespace CookWithMeSystem.Models
{
    using CookWithMeSystem.Common.Constants;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Recipe : BaseEntity
    {
        private ICollection<Ingredient> ingredients;
        private ICollection<Comment> comments;
        
        [StringLength(ValidationConstants.MaxRecipeTitle)]
        public string Title { get; set; }
        
        [StringLength(ValidationConstants.MaxRecipePreparation)]
        public string Preparation { get; set; }

        public int EstimationTime { get; set; }

        public string PublisherId { get; set; }

        public virtual User Publisher { get; set; }

        public bool IsPrivate { get; set; }

        public Recipe()
        {
            this.ingredients = new HashSet<Ingredient>();
            this.comments = new HashSet<Comment>();
        }

        public virtual ICollection<Ingredient> Ingredients
        {
            get { return this.ingredients; }
            set { this.ingredients = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

    }
}
