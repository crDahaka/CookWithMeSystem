namespace CookWithMe.API.Models.Recipes
{
    using System;
    using System.Collections.Generic;

    using CookWithMe.API.Infrastructure;
    using CookWithMeSystem.Models;
    using CookWithMe.API.Models.Ingredients;
    using AutoMapper;

    public class RecipeDetailsResponseModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int EstimationTime { get; set; }

        public string Preparation { get; set; }

        //public ICollection<IngredientViewModel> Ingredients { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<Recipe, RecipeDetailsResponseModel>();
        }
    }
}