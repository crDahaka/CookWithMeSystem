namespace CookWithMe.API.Models.Recipes
{
    using CookWithMe.API.Infrastructure;
    using CookWithMeSystem.Models;

    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using CookWithMe.API.Models.Ingredients;
    using System.Collections.Generic;

    public class SaveRecipeRequestModel : IMapFrom<Recipe>, IHaveCustomMappings
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
        
        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<Recipe, RecipeResponseModel>()
                .ForMember(r => r.Ingredients, opt => opt.MapFrom(i => i.Ingredients));
        }
    }
}