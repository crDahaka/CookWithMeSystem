namespace CookWithMe.API.Models.Recipes
{
    using CookWithMe.API.Infrastructure;
    using CookWithMeSystem.Models;

    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using CookWithMeSystem.Common.Constants;
    using AutoMapper;
    using CookWithMe.API.Models.Ingredients;
    using CookWithMe.API.Models.Steps;

    public class AddRecipeViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        [Required]
        [StringLength(ValidationConstants.MaxRecipeTitle, ErrorMessage = ValidationConstants.ValidationErrorMessage, MinimumLength = ValidationConstants.MinRecipeTitle)]
        public string Title { get; set; }
        
        [Required]
        [StringLength(ValidationConstants.MaxRecipeDirections, ErrorMessage = ValidationConstants.ValidationErrorMessage, MinimumLength = ValidationConstants.MinRecipeDirections)]
        public string Directions { get; set; }

        [Required]
        [Range(ValidationConstants.MinPreparationTime, ValidationConstants.MaxPreparationTime)]
        public byte PreparationTime { get; set; }

        [Required]
        [Range(ValidationConstants.MinCookTime, ValidationConstants.MaxCookTime)]
        public int CookTime { get; set; }
        
        [Required]
        [Range(ValidationConstants.MinRecipeServings, ValidationConstants.MaxRecipeServings)]
        public byte ServingsCount { get; set; }
        
        [Required]
        public bool IsPrivate { get; set; }

        [Required]
        public DifficultyLevel Level { get; set; }

        [Required]
        public ICollection<IngredientViewModel> Ingredients { get; set; }

        [Required]
        public ICollection<StepViewModel> Steps { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<AddRecipeViewModel, Recipe>()
                .ForMember(r => r.Ingredients, opt => opt.Ignore())
                .ForMember(r => r.Steps, opt => opt.Ignore());
            
        }
    }
}