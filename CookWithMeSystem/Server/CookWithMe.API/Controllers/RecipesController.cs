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
        [ValidationModelState]
        [Route("create")]
        public IHttpActionResult CreateRecipe(SaveRecipeRequestModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest("Invalid Model");
            }

            var createdRecipeId = this.recipes.Add(
                model.Title,
                model.EstimationTime,
                model.Preparation,
                this.User.Identity.GetUserId(),
                model.Ingredients,
                model.IsPrivate);

            
            return this.Ok(createdRecipeId);
        }
    }
}
