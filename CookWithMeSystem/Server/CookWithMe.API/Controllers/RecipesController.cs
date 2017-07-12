namespace CookWithMe.API.Controllers
{
    using CookWithMe.API.Infrastructure.ValidationAttributes;
    using CookWithMe.API.Models.Recipes;
    using CookWithMeSystem.Common.Constants;
    using CookWithMeSystem.Services.Contracts;
    using Microsoft.AspNet.Identity;
    using System.Web.Http;
    using System.Net;
    using System.Net.Http;
    using CookWithMeSystem.Models;
    using AutoMapper;
    using System.Collections.Generic;
    using CookWithMe.API.Models;
    using CookWithMeSystem.Data;

    [RoutePrefix("api/recipes")]
    public class RecipesController : BaseController
    {
        private readonly IRecipeService recipeService;

        public RecipesController(ICookWithMeSystemData data, IRecipeService recipeService)
            :base(data)
        {
            this.recipeService = recipeService;
        }

        public HttpResponseMessage GetAllRecipes()
        {
            var recipes = Mapper.Map<ICollection<RecipeDetailsViewModel>>(this.Data.Recipes.All());
            
            return this.CreateSerializedJsonResponse(HttpStatusCode.OK, recipes);
        }

        [HttpGet]
        [Route("all")]
        public HttpResponseMessage GetAllRecipes(int page, int pageSize = GlobalConstants.DefaultPageSize)
        {
            var recipes = Mapper.Map<ICollection<RecipeDetailsViewModel>>(this.recipeService.All(page, pageSize));

            return this.CreateSerializedJsonResponse(HttpStatusCode.OK, recipes);
        }

        [HttpGet]
        [Route("details/{id:int}")]
        public HttpResponseMessage GetRecipeDetails(int id)
        {
            var recipe = Mapper.Map<RecipeDetailsViewModel>(this.Data.Recipes.GetById(id));

            if (recipe == null)
            {
                return this.CreateSerializedJsonResponse(HttpStatusCode.NotFound, new ErrorViewModel { Message = GlobalConstants.RecipeNotFoundErrorMessage });
            }

            return this.CreateSerializedJsonResponse(HttpStatusCode.OK, recipe);
        }

        [Authorize]
        [HttpPost]
        [ValidationModelState]
        [Route("create")]
        public HttpResponseMessage CreateRecipe([FromBody]AddRecipeViewModel model)
        {
            if (User.Identity.GetUserId() == null)
            {
                return this.CreateSerializedJsonResponse(HttpStatusCode.Unauthorized, null);
            }

            var mappedRecipe = Mapper.Map<AddRecipeViewModel, Recipe>(model);
            var mappedIngredients = Mapper.Map<ICollection<Ingredient>>(model.Ingredients);
            var mappedSteps = Mapper.Map<ICollection<Step>>(model.Steps);

            this.recipeService.Add(mappedRecipe, User.Identity.GetUserId(), mappedIngredients, mappedSteps);
            return this.CreateSerializedJsonResponse(HttpStatusCode.OK, new { Message = GlobalConstants.SuccessCreateMessage });
        }

        [Authorize]
        [HttpPut]
        [ValidationModelState]
        [Route("update/{id:int}")]
        public HttpResponseMessage UpdateRecipe(int id, [FromBody]UpdateRecipeViewModel model)
        {
            var recipe = this.recipeService.GetById(id);

            if (recipe == null)
            {
                return this.CreateSerializedJsonResponse(HttpStatusCode.NotFound, new ErrorViewModel { Message = GlobalConstants.RecipeNotFoundErrorMessage });
            }
            
            Mapper.Map<UpdateRecipeViewModel, Recipe>(model, recipe);
            var mappedIngredients = Mapper.Map<ICollection<Ingredient>>(model.Ingredients);
            var mappedSteps = Mapper.Map<ICollection<Step>>(model.Steps);

            this.recipeService.Update(recipe, mappedIngredients, mappedSteps);
            return this.CreateSerializedJsonResponse(HttpStatusCode.OK, GlobalConstants.SuccessUpdateMessage);
        }

        [Authorize]
        [HttpDelete]
        [Route("delete/{id:int}")]
        public HttpResponseMessage DeleteRecipe(int id)
        {
            var dbRecipe = this.Data.Recipes.GetById(id);

            if (dbRecipe == null)
            {
                return this.CreateSerializedJsonResponse(HttpStatusCode.NotFound, new ErrorViewModel { Message = GlobalConstants.RecipeNotFoundErrorMessage });
            }

            this.recipeService.Delete(id);
            return this.CreateSerializedJsonResponse(HttpStatusCode.OK, new { Message = GlobalConstants.SuccessDeleteMessage });

        }

    }
}
