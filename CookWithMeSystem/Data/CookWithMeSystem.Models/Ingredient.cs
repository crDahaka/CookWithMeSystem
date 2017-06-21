namespace CookWithMeSystem.Models
{
    using CookWithMeSystem.Common.Constants;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Ingredient : BaseEntity
    {
        [Required]
        [StringLength(ValidationConstants.MaxIngredientName)]
        public string Name { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }

        public Ingredient()
        {
            this.Recipes = new HashSet<Recipe>();
        }

    }
}
