namespace CookWithMeSystem.Models
{
    using CookWithMeSystem.Common.Constants;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category : BaseEntity
    {
        private ICollection<Recipe> recipes;

        [StringLength(ValidationConstants.MaxCategoryName)]
        public string Name { get; set; }

        public Category()
        {
            this.recipes = new HashSet<Recipe>();
        }

        public virtual ICollection<Recipe> Recipes
        {
            get { return this.recipes; }
            set { this.recipes = value; }
        }
    }
}
