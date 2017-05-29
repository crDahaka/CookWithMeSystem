namespace CookWithMeSystem.Services
{
    using System.Linq;

    using CookWithMeSystem.Models;
    using CookWithMeSystem.Services.Contracts;
    using CookWithMeSystem.Data;

    public class RecipeService : IRecipeService
    {

        private readonly IRepository<Recipe> recipes;

        public RecipeService(IRepository<Recipe> recipesRepo)
        {
            this.recipes = recipesRepo;
        }

        public int Add(string title, int estimationTime, string preparation, bool isPrivate = false)
        {
            var newRecipe = new Recipe
            {
                Title = title,
                EstimationTime = estimationTime,
                Preparation = preparation,
                IsPrivate = isPrivate
            };

            this.recipes.Add(newRecipe);
            this.recipes.SaveChanges();

            return newRecipe.Id;
        }

        public IQueryable<Recipe> All(int page = 1, int pageSize = 10)
        {
            return this.recipes
                .All()
                .OrderByDescending(r => r.CreationDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
