namespace CookWithMeSystem.Models
{
    using CookWithMeSystem.Common.Constants;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category : BaseEntity
    {
        
        public string Name { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }

        public Category()
        {
            this.Recipes = new HashSet<Recipe>();
        }
        
    }
}
