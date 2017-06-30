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
        [StringLength(ValidationConstants.MaxRecipeTitle, ErrorMessage = "{0} should be at least {2} characters long.", MinimumLength = ValidationConstants.MinRecipeTitle)]
        public string Title { get; set; }
        
        [Required]
        //[StringLength(ValidationConstants.MaxRecipeDirections, ErrorMessage = "{0} should be at least {2} characters long.", MinimumLength = ValidationConstants.MinRecipeDescription)]
        public string Directions { get; set; }

        [Required]
        public int PreparationTime { get; set; }

        [Required]
        public byte ServingsCount { get; set; }
        
        [Required]
        public bool IsPrivate { get; set; }

        [Required]
        public ICollection<Ingredient> Ingredients { get; set; }

        [Required]
        public ICollection<Step> Steps { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<AddRecipeViewModel, Recipe>()
                .ForMember(r => r.Ingredients, opt => opt.Ignore())
                .ForMember(r => r.Steps, opt => opt.Ignore());
            
        }
    }
}