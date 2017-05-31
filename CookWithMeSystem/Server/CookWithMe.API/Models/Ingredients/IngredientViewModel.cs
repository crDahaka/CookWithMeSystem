namespace CookWithMe.API.Models.Ingredients
{
    using AutoMapper;

    using CookWithMe.API.Infrastructure;
    using CookWithMeSystem.Models;

    public class IngredientViewModel : IMapFrom<Ingredient>
    {
        public int Id { get; set; }

        public string Name { get; set; }

    //    public void CreateMappings(IConfiguration config)
    //    {
    //        config.CreateMap<Ingredient, IngredientViewModel>()
    //            .ForMember(m => m.Name, opt => opt.MapFrom(n => n.Name))
    //            .ReverseMap();
    //    }
    }
}