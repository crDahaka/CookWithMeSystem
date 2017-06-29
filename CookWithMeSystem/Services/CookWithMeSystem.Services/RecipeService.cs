namespace CookWithMeSystem.Services
{
    using System.Linq;
    using CookWithMeSystem.Models;
    using CookWithMeSystem.Services.Contracts;
    using CookWithMeSystem.Data;
    using CookWithMeSystem.Common.Constants;
    using System.Collections.Generic;
    using AutoMapper;

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

        public void Add(Recipe recipe, string publisherID, ICollection<Ingredient> ingredients, ICollection<Step> steps)
        {
            var currentUser = this.users.All().FirstOrDefault(u => u.Id == publisherID);

            var newRecipe = new Recipe
            {
                Title = recipe.Title,
                Directions = recipe.Directions,
                PreparationTime = recipe.PreparationTime,
                ServingsCount = recipe.ServingsCount,
                PublisherID = currentUser.Id,
                Ingredients = ingredients,
                Steps = steps,
                IsPrivate = recipe.IsPrivate
            };
            
            this.recipes.Add(newRecipe);
            this.recipes.SaveChanges();
        }

        public void Update(Recipe recipe, ICollection<Ingredient> ingredients, ICollection<Step> steps)
        {
            var dbRecipe = this.GetById(recipe.ID);
            
            AddOrUpdateIngredientStep(dbRecipe, ingredients, steps);

            this.recipes.Update(recipe);
            this.recipes.SaveChanges();
        }

        public void Delete(int id)
        {
            var recipe = this.GetById(id);

            this.recipes.Delete(recipe);
            this.recipes.SaveChanges();
        }

        private void AddOrUpdateIngredientStep(Recipe existingRecipe, ICollection<Ingredient> ingredients, ICollection<Step> steps)
        {
            var dbIngrs = existingRecipe.Ingredients;
            dbIngrs.Clear();

            foreach (var ingr in ingredients)
            {
                var existingIngredient = this.ingredients.All().FirstOrDefault(i => i.Name == ingr.Name);

                existingRecipe.Ingredients.Add(existingIngredient != null ? existingIngredient : Mapper.Map<Ingredient>(ingr));
            }
            
            foreach (var step in steps)
            {
                var existingStep = this.steps.All().FirstOrDefault(s => s.Action == step.Action);

                existingRecipe.Steps.Add(existingStep != null ? existingStep : Mapper.Map<Step>(step));

            }
            
        }
        
    }
}
