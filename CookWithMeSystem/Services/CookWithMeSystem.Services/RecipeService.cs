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

    public class RecipeService : BaseService, IRecipeService
    {
        public RecipeService(ICookWithMeSystemData data)
            :base(data)
        {
        }

        /// <summary>
        /// Return a single recipe entity by given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Recipe GetById(int id)
        {
            var dbRecipe = this.Data.Recipes.GetById(id);
            
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

            return this.Data.Recipes
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
            var currentUser = this.Data.Users.All().FirstOrDefault(u => u.Id == publisherID);

            var newRecipe = new Recipe
            {
                Title = recipe.Title,
                Overview = recipe.Overview,
                PreparationTime = recipe.PreparationTime,
                CookTime = recipe.CookTime,
                TotalTime = recipe.PreparationTime + recipe.CookTime,
                ServingsCount = recipe.ServingsCount,
                Level = recipe.Level,
                PublisherID = currentUser.Id,
                Ingredients = ingredients,
                Steps = steps,
                IsPrivate = recipe.IsPrivate
            };

            try
            {
                this.Data.Recipes.Add(newRecipe);
                this.Data.Recipes.SaveChanges();
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
                this.Data.Recipes.Update(recipe);
                this.Data.Recipes.SaveChanges();

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
                this.Data.Recipes.Delete(recipe);
                this.Data.Recipes.SaveChanges();
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
                var existingIngredient = this.Data.Ingredients.All().FirstOrDefault(i => i.Name == ingredient.Name);

                existingRecipe.Ingredients.Add(existingIngredient != null ? existingIngredient : Mapper.Map<Ingredient>(ingredient));
            }
            
            var dbSteps = existingRecipe.Steps;
            dbSteps.Clear();

            foreach (var step in steps)
            {
                var existingStep = this.Data.Steps.All().FirstOrDefault(s => s.Action == step.Action);

                existingRecipe.Steps.Add(existingStep != null ? existingStep : Mapper.Map<Step>(step));

            }
            
        }
        
    }
}
