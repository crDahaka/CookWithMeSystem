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
    using System.Data.Entity;

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

        [HttpDelete]
        [Route("delete/{id}")]
        public IHttpActionResult DeleteRecipe(int id)
        {
            this.recipes.Delete(id);

            return this.Ok();
        }
    }
}
