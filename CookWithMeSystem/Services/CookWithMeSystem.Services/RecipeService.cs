namespace CookWithMeSystem.Services
{
    using System.Linq;
    using CookWithMeSystem.Models;
    using CookWithMeSystem.Services.Contracts;
    using CookWithMeSystem.Data;
    using CookWithMeSystem.Common.Constants;
    using System.Collections.Generic;

    public class RecipeService : IRecipeService
    {
        private readonly IRepository<Recipe> recipes;
        private readonly IRepository<User> users;
        private readonly IRepository<Ingredient> ingredients;
        private readonly IRepository<Step> steps;

        public RecipeService(
            IRepository<Recipe> recipesRepo, IRepository<User> usersRepo, IRepository<Ingredient> ingredientsRepo, IRepository<Step> stepsRepo)
        {
            this.recipes = recipesRepo;
            this.users = usersRepo;
            this.ingredients = ingredientsRepo;
            this.steps = stepsRepo;
        }

        public Recipe GetById(int id)
        {
            var dbRecipe = this.recipes.GetById(id);

            //if (dbRecipe != null)
            //{
            //    this.recipes.Attach(dbRecipe);
            //}

            return dbRecipe;
        }

        public IQueryable<Recipe> All(int page = 1, int pageSize = GlobalConstants.DefaultPageSize)
        {
            return this.recipes
                .All()
                .OrderByDescending(r => r.Title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }

        public void Add(string title, string description, string publisherId, ICollection<Ingredient> ingredients, ICollection<Step> steps, bool isPrivate = false)
        {
            var currentUser = this.users.All().FirstOrDefault(u => u.Id == publisherId);
            
            var newRecipe = new Recipe
            {
                Title = title,
                Description = description,
                PublisherID = currentUser.Id,
                Ingredients = ingredients,
                Steps = steps,
                IsPrivate = isPrivate
            };
            
            this.recipes.Add(newRecipe);
            this.recipes.SaveChanges();
        }
        
        public void Update(Recipe recipe)
        {

            this.recipes.Update(recipe);
            this.recipes.SaveChanges();
        }

        public void Delete(int id)
        {
            var recipe = this.GetById(id);

            this.recipes.Delete(recipe);
            this.recipes.SaveChanges();
        }
        
    }
}
