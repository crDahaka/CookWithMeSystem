namespace CookWithMe.API.Models.Recipes
{
    using CookWithMe.API.Infrastructure;
    using CookWithMeSystem.Common.Constants;
    using CookWithMeSystem.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using CookWithMe.API.Models.Steps;
    using CookWithMe.API.Models.Ingredients;

    public class UpdateRecipeViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        [Required]
        [StringLength(ValidationConstants.MaxRecipeTitle, ErrorMessage = ValidationConstants.RecipeErrorMessage, MinimumLength = ValidationConstants.MinRecipeTitle)]
        public string Title { get; set; }

        [Required]
        [StringLength(ValidationConstants.MaxRecipeDirections, ErrorMessage = ValidationConstants.RecipeErrorMessage, MinimumLength = ValidationConstants.MinRecipeDirections)]
        public string Directions { get; set; }

        [Required]
        [Range(ValidationConstants.MinPreparationTime, ValidationConstants.MaxPreparationTime, ErrorMessage = ValidationConstants.RecipeErrorMessage)]
        public int PreparationTime { get; set; }

        [Required]
        [Range(ValidationConstants.MinRecipeServings, ValidationConstants.MaxRecipeServings, ErrorMessage = ValidationConstants.RecipeErrorMessage)]
        public byte ServingsCount { get; set; }

        [Required]
        public bool IsPrivate { get; set; }

        [Required]
        public ICollection<IngredientViewModel> Ingredients { get; set; }

        [Required]
        public ICollection<StepViewModel> Steps { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<UpdateRecipeViewModel, Recipe>()
                .ForMember(r => r.Ingredients, opt => opt.Ignore())
                .ForMember(r => r.Steps, opt => opt.Ignore());
        }
    }
}