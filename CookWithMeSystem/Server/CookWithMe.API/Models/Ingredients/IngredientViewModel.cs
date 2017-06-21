namespace CookWithMe.API.Models.Ingredients
{
    using CookWithMe.API.Infrastructure;
    using CookWithMeSystem.Models;

    public class IngredientViewModel : IMapFrom<Ingredient>
    {
        public int ID { get; set; }

        public string Name { get; set; }
        
    }
}