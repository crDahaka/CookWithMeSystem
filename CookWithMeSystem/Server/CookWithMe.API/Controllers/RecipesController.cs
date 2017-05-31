namespace CookWithMe.API.Controllers
{
    using AutoMapper.QueryableExtensions;
    using CookWithMe.API.Models.Recipes;
    using CookWithMeSystem.Common.Constants;
    using CookWithMeSystem.Services.Contracts;

    using System.Linq;
    using System.Web.Http;

    public class RecipesController : ApiController
    {
        private readonly IRecipeService recipes;

        public RecipesController(IRecipeService recipeService)
        {
            this.recipes = recipeService;
        }

        public IHttpActionResult Get()
        {
            var result = this.recipes
                .All(page: 1)
                .Project()
                .To<RecipeResponseModel>()
                .ToList();

            return this.Ok(result);
        }

        [Route("api/recipes/all")]
        public IHttpActionResult Get(int page, int pageSize = GlobalConstants.DefaultPageSize)
        {
            var result = this.recipes
                .All(page, pageSize)
                .Project()
                .To<RecipeResponseModel>()
                .ToList();

            return this.Ok(result);
        }

        [Route("api/recipe/add")]
        public IHttpActionResult Post(SaveRecipeRequestModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var createdRecipeId = this.recipes.Add(
                model.Title,
                model.EstimationTime,
                model.Preparation,
                model.IsPrivate);
            
            return this.Ok(createdRecipeId);
        }
    }
}
