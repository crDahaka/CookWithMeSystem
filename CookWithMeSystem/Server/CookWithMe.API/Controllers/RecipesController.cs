﻿namespace CookWithMe.API.Controllers
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
            //TODO: prevent negative page number exception
            var result = Mapper.Map<IList<RecipeDetailsViewModel>>(this.recipeService.All(page, pageSize));
            
            return this.Ok(result);
        }
        
        [HttpGet]
        [Route("details/{id:int}")]
        public HttpResponseMessage GetRecipeDetails(int id)
        {
            var recipe =  Mapper.Map<RecipeDetailsViewModel>(this.recipeService.GetById(id));

            if (recipe == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError(ValidationConstants.RecipeNotFoundErrorMessage));
            }

            return Request.CreateResponse(HttpStatusCode.OK, recipe);
        }

        [Authorize]
        [HttpPost]
        [ValidationModelState]
        [Route("create")]
        public IHttpActionResult CreateRecipe([FromBody]AddRecipeViewModel model)
        {
            if (User.Identity.GetUserId() == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            var mappedRecipe = Mapper.Map<AddRecipeViewModel, Recipe>(model);
            var mappedIngredients = Mapper.Map<ICollection<Ingredient>>(model.Ingredients);
            var mappedSteps = Mapper.Map<ICollection<Step>>(model.Steps);

            this.recipeService.Add(mappedRecipe, User.Identity.GetUserId(), mappedIngredients, mappedSteps);

            return this.Ok(mappedRecipe);
        }

        [Authorize]
        [HttpPut]
        [ValidationModelState]
        [Route("update/{id:int}")]
        public IHttpActionResult UpdateRecipe(int id, [FromBody]UpdateRecipeViewModel model)
        {
            var recipe = this.recipeService.GetById(id);

            if (recipe == null)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ValidationConstants.RecipeNotFoundErrorMessage));
            }

            Mapper.Map<UpdateRecipeViewModel, Recipe>(model, recipe);
            var mappedIngredients = Mapper.Map<ICollection<Ingredient>>(model.Ingredients);
            var mappedSteps = Mapper.Map<ICollection<Step>>(model.Steps);

            this.recipeService.Update(recipe, mappedIngredients, mappedSteps);

            return Ok();
        }

        [Authorize]
        [HttpDelete]
        [Route("delete/{id:int}")]
        public IHttpActionResult DeleteRecipe(int id)
        {
            var dbRecipe = this.recipeService.GetById(id);

            if (dbRecipe == null)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ValidationConstants.RecipeNotFoundErrorMessage));
            }

            this.recipeService.Delete(id);

            return this.Ok();
        }
        
    }
}
