namespace CookWithMe.API.Models.Ingredients
{
    using CookWithMe.API.Infrastructure;
    using CookWithMeSystem.Common.Constants;
    using CookWithMeSystem.Models;
    using System.ComponentModel.DataAnnotations;

    public class IngredientViewModel : IMapFrom<Ingredient>
    {
        public int ID { get; set; }

        [StringLength(ValidationConstants.MaxIngredientName, ErrorMessage = ValidationConstants.ValidationErrorMessage, MinimumLength = ValidationConstants.MinIngredientName)]
        public string Name { get; set; }
        
    }
}