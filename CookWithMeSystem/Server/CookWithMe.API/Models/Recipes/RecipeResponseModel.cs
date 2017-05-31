namespace CookWithMe.API.Models.Recipes
{
    using CookWithMe.API.Infrastructure;
    using CookWithMe.API.Models.Ingredients;
    using CookWithMeSystem.Models;
    using System.Collections.Generic;
    using AutoMapper;
    using System;

    public class RecipeResponseModel : IMapFrom<Recipe>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int EstimationTime { get; set; }

        public string Preparation { get; set; }

        public bool IsPrivate { get; set; }

        public ICollection<IngredientViewModel> Ingrs { get; set; }

        //public void CreateMappings(IConfiguration config)
        //{
        //    config.CreateMap<Recipe, RecipeResponseModel>()
        //        .ForMember(i => i.Ingrs, opt => opt.MapFrom(i => i.Ingredients));
        //}
    }
} 