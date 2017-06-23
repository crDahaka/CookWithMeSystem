namespace CookWithMeSystem.Services
{
    using System.Linq;
    using CookWithMeSystem.Models;
    using CookWithMeSystem.Services.Contracts;
    using CookWithMeSystem.Data;
    using CookWithMeSystem.Common.Constants;
    using System.Collections.Generic;
    using AutoMapper;
    using System;

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

        //public void Add(string title, string description, string publisherId, ICollection<Ingredient> ingredients, ICollection<Step> steps, bool isPrivate = false)
        //{
        //    var currentUser = this.users.All().FirstOrDefault(u => u.Id == publisherId);

        //    var newRecipe = new Recipe
        //    {
        //        Title = title,
        //        Directions = description,
        //        PublisherID = currentUser.Id,
        //        Ingredients = ingredients,
        //        Steps = steps,
        //        IsPrivate = isPrivate
        //    };

        //    this.recipes.Add(newRecipe);
        //    this.recipes.SaveChanges();
        //}

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

        public void Update(Recipe recipe)
        {
            var dbRecipe = this.GetById(recipe.ID);

            this.AddOrUpdateIngredientStep(dbRecipe, recipe);

            this.recipes.Update(recipe);
            this.recipes.SaveChanges();
        }

        public void Delete(int id)
        {
            var recipe = this.GetById(id);

            this.recipes.Delete(recipe);
            this.recipes.SaveChanges();
        }

        private void AddOrUpdateIngredientStep(Recipe existingRecipe, Recipe recipeToCheck)
        {
            foreach (var ingr in recipeToCheck.Ingredients)
            {
                var existingIngredient = existingRecipe.Ingredients.FirstOrDefault(i => i.Name == ingr.Name);
                if (existingIngredient != null)
                {
                    Mapper.Map(ingr, existingIngredient);
                }
                else
                {
                    existingRecipe.Ingredients.Add(Mapper.Map<Ingredient>(ingr));
                }
            }

            foreach (var step in recipeToCheck.Steps)
            {
                var existingStep = existingRecipe.Steps.FirstOrDefault(s => s.Action == step.Action);
                if (existingStep != null)
                {
                    Mapper.Map(step, existingStep);
                }
                else
                {
                    existingRecipe.Steps.Add(Mapper.Map<Step>(step));
                }
            }
        }
        
    }
}
