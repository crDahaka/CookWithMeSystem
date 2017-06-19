namespace CookWithMe.API.Models.Recipes
{
    using CookWithMe.API.Infrastructure;
    using CookWithMeSystem.Models;

    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using CookWithMeSystem.Common.Constants;

    public class SaveRecipeRequestModel : IMapFrom<Recipe>
    {
        [Required]
        [StringLength(ValidationConstants.MaxRecipeTitle, ErrorMessage = "{0} should be at least {2} characters long.", MinimumLength = ValidationConstants.MinRecipeTitle)]
        public string Title { get; set; }
        
        [Required]
        [StringLength(ValidationConstants.MaxRecipePreparation, ErrorMessage = "{0} should be at least {2} characters long.", MinimumLength = ValidationConstants.MinRecipePreparation)]
        public string Preparation { get; set; }
        
        [Required]
        public bool IsPrivate { get; set; }

        [Required]
        public ICollection<Ingredient> Ingredients { get; set; }
        
    }
}