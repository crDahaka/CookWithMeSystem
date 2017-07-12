namespace CookWithMe.API.Models.Recipes
{
    using CookWithMe.API.Infrastructure;
    using CookWithMe.API.Models.Ingredients;
    using CookWithMeSystem.Models;
    using System.Collections.Generic;
    using AutoMapper;
    using CookWithMe.API.Models.Steps;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class RecipeDetailsViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public byte PreparationTime { get; set; }

        public int CookTime { get; set; }

        public int TotalTime { get; set; }

        public string Directions { get; set; }

        public byte ServingsCount { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DifficultyLevel Level { get; set; }

        public string Publisher { get; set; }

        public string CreationDate { get; set; }

        public bool IsPrivate { get; set; }

        public ICollection<IngredientViewModel> Ingredients { get; set; }

        public ICollection<StepViewModel> Steps { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<Recipe, RecipeDetailsViewModel>()
                .ForMember(r => r.Ingredients, opt => opt.MapFrom(i => i.Ingredients))
                .ForMember(r => r.Steps, opt => opt.MapFrom(s => s.Steps))
                .ForMember(r => r.Publisher, opt => opt.MapFrom(u => u.Publisher.UserName))
                .ReverseMap();
        }
    }
} 