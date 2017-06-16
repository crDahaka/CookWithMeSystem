namespace CookWithMe.API.Models.Recipes
{
    using CookWithMe.API.Infrastructure;
    using CookWithMeSystem.Models;

    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class SaveRecipeRequestModel : IMapFrom<Recipe>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int EstimationTime { get; set; }

        [Required]
        public string Preparation { get; set; }
        
        [Required]
        public bool IsPrivate { get; set; }

        [Required]
        public ICollection<Ingredient> Ingredients { get; set; }
        
    }
}