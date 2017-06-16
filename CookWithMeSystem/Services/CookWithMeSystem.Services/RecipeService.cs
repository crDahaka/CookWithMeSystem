namespace CookWithMeSystem.Services
{
    using System.Linq;

    using CookWithMeSystem.Models;
    using CookWithMeSystem.Services.Contracts;
    using CookWithMeSystem.Data;
    using CookWithMeSystem.Common.Constants;
    using System.Collections.Generic;
    using System;

    public class RecipeService : IRecipeService
    {
        private readonly IRepository<Recipe> recipes;
        private readonly IRepository<User> users;
        private readonly IRepository<Ingredient> ingredients;

        public RecipeService(IRepository<Recipe> recipesRepo, IRepository<User> usersRepo, IRepository<Ingredient> ingredientsRepo)
        {
            this.recipes = recipesRepo;
            this.users = usersRepo;
            this.ingredients = ingredientsRepo;
        }

        public int Add(string title, int estimationTime, string preparation, string publisherId, ICollection<Ingredient> ingredients, bool isPrivate = false)
        {
            var currentUser = this.users.All().FirstOrDefault(u => u.Id == publisherId);

            var newRecipe = new Recipe
            {
                Title = title,
                EstimationTime = estimationTime,
                Preparation = preparation,
                PublisherId = currentUser.Id,
                Ingredients = ingredients,
                IsPrivate = isPrivate
            };

            foreach (Ingredient ingredient in ingredients)
            {
                if (string.IsNullOrEmpty(ingredient.Name) || string.IsNullOrWhiteSpace(ingredient.Name))
                {
                    throw new ArgumentNullException("Ingredient name cannot be empty.");
                }

                this.ingredients.Add(ingredient);
                ingredient.Recipe = newRecipe;
            }
            
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
