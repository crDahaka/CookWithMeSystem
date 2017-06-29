namespace CookWithMe.API.Models.Recipes
{
    using CookWithMe.API.Infrastructure;
    using CookWithMeSystem.Common.Constants;
    using CookWithMeSystem.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;

    public class UpdateRecipeViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        [Required]
        //[StringLength(ValidationConstants.MaxRecipeTitle, ErrorMessage = "{0} should be at least {2} characters long.", MinimumLength = ValidationConstants.MinRecipeTitle)]
        public string Title { get; set; }

        [Required]
        //[StringLength(ValidationConstants.MaxRecipeDescription, ErrorMessage = "{0} should be at least {2} characters long.", MinimumLength = ValidationConstants.MinRecipeDescription)]
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
            config.CreateMap<UpdateRecipeViewModel, Recipe>()
                .ForMember(r => r.Ingredients, opt => opt.Ignore())
                .ForMember(r => r.Steps, opt => opt.Ignore());
        }
    }
}