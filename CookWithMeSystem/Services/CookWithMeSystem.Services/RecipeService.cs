namespace CookWithMeSystem.Services
{
    using System.Linq;
    using CookWithMeSystem.Models;
    using CookWithMeSystem.Services.Contracts;
    using CookWithMeSystem.Data;
    using CookWithMeSystem.Common.Constants;
    using System.Collections.Generic;
    using AutoMapper;
    using System.Data.Entity.Validation;

    public class RecipeService : IRecipeService
    {
        private readonly IRepository<Recipe> recipes;
        private readonly IRepository<User> users;
        private readonly IRepository<Ingredient> ingredients;
        private readonly IRepository<Step> steps;

        /// <summary>
        /// Recipe service constructor.
        /// </summary>
        /// <param name="recipesRepo"></param>
        /// <param name="usersRepo"></param>
        /// <param name="ingredientsRepo"></param>
        /// <param name="stepsRepo"></param>
        public RecipeService(
            IRepository<Recipe> recipesRepo, IRepository<User> usersRepo, IRepository<Ingredient> ingredientsRepo, IRepository<Step> stepsRepo)
        {
            this.recipes = recipesRepo;
            this.users = usersRepo;
            this.ingredients = ingredientsRepo;
            this.steps = stepsRepo;
        }

        /// <summary>
        /// Return a single recipe entity by given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Recipe GetById(int id)
        {
            var dbRecipe = this.recipes.GetById(id);
            
            return dbRecipe;
        }

        /// <summary>
        /// Retrieves a list of recipes from the db.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IQueryable<Recipe> All(int page = 1, int pageSize = GlobalConstants.DefaultPageSize)
        {
            if (page <= 0) page = 1;

            return this.recipes
                .All()
                .OrderByDescending(r => r.CreationDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }

        /// <summary>
        /// Creates new recipe.
        /// </summary>
        /// <param name="recipe"></param>
        /// <param name="publisherID"></param>
        /// <param name="ingredients"></param>
        /// <param name="steps"></param>
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

            try
            {
                this.recipes.Add(newRecipe);
                this.recipes.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
            
        }

        /// <summary>
        /// Save the updated recipe.
        /// </summary>
        /// <param name="recipe"></param>
        /// <param name="ingredients"></param>
        /// <param name="steps"></param>
        public void Update(Recipe recipe, ICollection<Ingredient> ingredients, ICollection<Step> steps)
        {
            var dbRecipe = this.GetById(recipe.ID);
            
            AddOrUpdateIngredientStep(dbRecipe, ingredients, steps);

            try
            {
                this.recipes.Update(recipe);
                this.recipes.SaveChanges();

            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
            
        }

        /// <summary>
        /// Delete a single recipe entity by given id;
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var recipe = this.GetById(id);

            try
            {
                this.recipes.Delete(recipe);
                this.recipes.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
            
        }

        /// <summary>
        /// Check if the recipe contains existing ingredient/step, then add or update it.
        /// </summary>
        /// <param name="existingRecipe"></param>
        /// <param name="ingredients"></param>
        /// <param name="steps"></param>
        private void AddOrUpdateIngredientStep(Recipe existingRecipe, ICollection<Ingredient> ingredients, ICollection<Step> steps)
        {
            var dbIngrs = existingRecipe.Ingredients;
            dbIngrs.Clear();

            foreach (var ingredient in ingredients)
            {
                var existingIngredient = this.ingredients.All().FirstOrDefault(i => i.Name == ingredient.Name);

                existingRecipe.Ingredients.Add(existingIngredient != null ? existingIngredient : Mapper.Map<Ingredient>(ingredient));
            }
            
            var dbSteps = existingRecipe.Steps;
            dbSteps.Clear();

            foreach (var step in steps)
            {
                var existingStep = this.steps.All().FirstOrDefault(s => s.Action == step.Action);

                existingRecipe.Steps.Add(existingStep != null ? existingStep : Mapper.Map<Step>(step));

            }
            
        }
        
    }
}
