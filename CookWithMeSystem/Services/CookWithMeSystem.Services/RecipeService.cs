namespace CookWithMeSystem.Services
{
    using System.Linq;

    using CookWithMeSystem.Models;
    using CookWithMeSystem.Services.Contracts;
    using CookWithMeSystem.Data;
    using CookWithMeSystem.Common.Constants;

    public class RecipeService : IRecipeService
    {
        private readonly IRepository<Recipe> recipes;
        private readonly IRepository<User> users;

        public RecipeService(IRepository<Recipe> recipesRepo, IRepository<User> usersRepo)
        {
            this.recipes = recipesRepo;
            this.users = usersRepo;
        }

        public int Add(string title, int estimationTime, string preparation, string publisherId, bool isPrivate = false)
        {
            var currentUser = this.users
                .All()
                .FirstOrDefault(u => u.Id == publisherId);

            var newRecipe = new Recipe
            {
                Title = title,
                EstimationTime = estimationTime,
                Preparation = preparation,
                PublisherId = currentUser.Id,
                IsPrivate = isPrivate
            };
            
            this.recipes.Add(newRecipe);
            this.recipes.SaveChanges();

            return newRecipe.Id;
        }

        public IQueryable<Recipe> All(int page = 1, int pageSize = GlobalConstants.DefaultPageSize)
        {
            return this.recipes
                .All()
                .OrderByDescending(r => r.Title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
