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
    using JsonPatch;
    using System.Web.Http.OData;
    using AutoMapper;

    [RoutePrefix("api/recipes")]
    public class RecipesController : ApiController
    {
        private readonly IRecipeService recipes;

        public RecipesController(IRecipeService recipeService)
        {
            this.recipes = recipeService;
        }

        public IHttpActionResult GetAllRecipes()
        {
            var result = this.recipes
                .All(page: 1)
                .ProjectTo<RecipeResponseModel>()
                .ToList();

            return this.Ok(result);
        }

        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetAllRecipes (int page, int pageSize = GlobalConstants.DefaultPageSize)
        {
            var result = this.recipes
                .All(page, pageSize)
                .ProjectTo<RecipeResponseModel>()
                .ToList();

            return this.Ok(result);
        }

        [Authorize]
        [HttpPost]
        [ValidationModelState]
        [Route("create")]
        public IHttpActionResult CreateRecipe([FromBody]SaveRecipeRequestModel model)
        {
            if (!this.ModelState.IsValid || model == null)
            {
                return this.BadRequest("Invalid Model");
            }

            if (User.Identity.GetUserId() == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            this.recipes.Add(model.Title, model.Description,User.Identity.GetUserId(), model.Ingredients, model.Steps, model.IsPrivate);
            
            return this.Ok();
        }

        //[Authorize]
        //[HttpPatch]
        //[ValidationModelState]
        //[Route("update/{id}")]
        //public IHttpActionResult Patch(int id, [FromBody]Recipe recipe)
        //{

        //    var dbRecipe = this.recipes.GetById(id);

        //    if (dbRecipe == null)
        //    {
        //        throw new HttpResponseException(HttpStatusCode.NotFound);
        //    }

        //    this.recipes.Update(recipe);

        //    return this.Ok();
        //}



        [HttpPut]
        [Route("update/{id}")]
        public IHttpActionResult UpdateRecipe(int id,[FromBody]SaveRecipeRequestModel model)
        {
            var recipe = this.recipes.GetById(id);

            if (recipe == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map<SaveRecipeRequestModel, Recipe>(model, recipe);
            this.recipes.Update(recipe);

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IHttpActionResult DeleteRecipe(int id)
        {
            var dbRecipe = this.recipes.GetById(id);

            if (dbRecipe == null)
            {
                var message = string.Format("Recipe with id {0} not found", id);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }

            this.recipes.Delete(id);

            return this.Ok();
        }
    }
}
