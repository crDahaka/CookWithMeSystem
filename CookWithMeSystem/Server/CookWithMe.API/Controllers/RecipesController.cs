namespace CookWithMe.API.Controllers
{
    using AutoMapper.QueryableExtensions;
    using CookWithMe.API.Infrastructure.ValidationAttributes;
    using CookWithMe.API.Models.Recipes;
    using CookWithMeSystem.Common.Constants;
    using CookWithMeSystem.Services.Contracts;
    using Microsoft.AspNet.Identity;
    using System.Linq;
    using System.Web.Http;
    using System.Net;
    using System.Net.Http;
    using CookWithMeSystem.Models;
    using AutoMapper;
    using System.Collections;
    using System.Collections.Generic;

    [RoutePrefix("api/recipes")]
    public class RecipesController : ApiController
    {
        private readonly IRecipeService recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
        }

        public IHttpActionResult GetAllRecipes()
        {
            var result = this.recipeService
                .All(page: 1)
                .ProjectTo<RecipeDetailsViewModel>()
                .ToList();

            return this.Ok(result);
        }

        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetAllRecipes (int page, int pageSize = GlobalConstants.DefaultPageSize)
        {
            var result = this.recipeService
                .All(page, pageSize)
                .ProjectTo<RecipeDetailsViewModel>()
                .ToList();

            return this.Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("details/{id}")]
        public IHttpActionResult GetRecipeDetails(int id)
        {
            var recipe = this.recipeService
                .All()
                .Where(r => r.ID == id)
                .ProjectTo<RecipeDetailsViewModel>()
                .FirstOrDefault();

            if (recipe == null)
            {
                var message = string.Format("Recipe with id {0} not found", id);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }

            return this.Ok(recipe);
        }

        [Authorize]
        [HttpPost]
        [ValidationModelState]
        [Route("create")]
        public IHttpActionResult CreateRecipe([FromBody]AddRecipeViewModel recipeModel)
        {
            if (!this.ModelState.IsValid || recipeModel == null)
            {
                return this.BadRequest("Invalid Model");
            }

            if (User.Identity.GetUserId() == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            var mappedRecipe = Mapper.Map<AddRecipeViewModel, Recipe>(recipeModel);
            this.recipeService.Add(mappedRecipe, User.Identity.GetUserId(), recipeModel.Ingredients, recipeModel.Steps);
            
            return this.Ok(mappedRecipe);
        }

        [Authorize]
        [HttpPut]
        [ValidationModelState]
        [Route("update/{id}")]
        public IHttpActionResult UpdateRecipe(int id, [FromBody]UpdateRecipeViewModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return this.BadRequest("Invalid Model");
            }
            var recipe = this.recipeService.GetById(id);

            if (recipe == null)
            {
                var message = string.Format("Recipe with id {0} not found", id);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }

            Mapper.Map<UpdateRecipeViewModel, Recipe>(model, recipe);

            this.recipeService.Update(recipe, model.Ingredients, model.Steps);

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IHttpActionResult DeleteRecipe(int id)
        {
            var dbRecipe = this.recipeService.GetById(id);

            if (dbRecipe == null)
            {
                var message = string.Format("Recipe with id {0} not found", id);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }

            this.recipeService.Delete(id);

            return this.Ok();
        }
    }
}
