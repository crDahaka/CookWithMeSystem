namespace CookWithMeSystem.Models
{
    using CookWithMeSystem.Common.Constants;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Recipe : BaseEntity
    {
        private ICollection<Ingredient> ingredients;
        private ICollection<Comment> comments;
        private ICollection<Vote> votes;
        
        [StringLength(ValidationConstants.MaxRecipeTitle)]
        public string Title { get; set; }
        
        [StringLength(ValidationConstants.MaxRecipePreparation)]
        public string Preparation { get; set; }

        public int EstimationTime { get; set; }

        public string PublisherId { get; set; }

        public virtual User Publisher { get; set; }

        public int? ImageId { get; set; }

        public virtual Image Image { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public bool IsPrivate { get; set; }

        public Recipe()
        {
            this.ingredients = new HashSet<Ingredient>();
            this.comments = new HashSet<Comment>();
            this.votes = new HashSet<Vote>();
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

        public virtual ICollection<Vote> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
        }

    }
}
