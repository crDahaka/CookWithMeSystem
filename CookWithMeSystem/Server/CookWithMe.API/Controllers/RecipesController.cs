namespace CookWithMe.API.Controllers
{
    using CookWithMe.API.Models.Recipes;
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
        

        [Route("api/recipes/all")]
        public IHttpActionResult Get(int page, int pageSize = 10)
        {
            var result = this.recipes
                .All()
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
