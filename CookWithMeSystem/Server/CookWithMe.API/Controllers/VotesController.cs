namespace CookWithMe.API.Controllers
{
    using CookWithMe.API.Models;
    using CookWithMeSystem.Common.Constants;
    using CookWithMeSystem.Data;
    using CookWithMeSystem.Models;
    using Microsoft.AspNet.Identity;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class VotesController : BaseController
    {
        public VotesController(ICookWithMeSystemData data)
            :base(data)
        {
        }

        [HttpPost]
        [Route("api/vote/{id:int}")]
        public HttpResponseMessage Vote(int id)
        {
            var userID = this.User.Identity.GetUserId();

            var dbRecipe = this.Data.Recipes.GetById(id);
            var canVote = !this.Data.Votes.All().Any(x => x.RecipeID == id && x.VotedByID == userID);

            if (dbRecipe == null)
            {
                return this.CreateSerializedJsonResponse(HttpStatusCode.NotFound, new ErrorViewModel { Message = GlobalConstants.RecipeNotFoundErrorMessage });
            }

            if (!canVote)
            {
                return this.CreateSerializedJsonResponse(HttpStatusCode.NotAcceptable, new ErrorViewModel { Message = GlobalConstants.AlreadyVotedMessage });
            }

            dbRecipe.Votes.Add(new Vote
            {
                RecipeID = id,
                VotedByID = userID
            });
            

            this.Data.SaveChanges();
            return this.CreateSerializedJsonResponse(HttpStatusCode.OK, new { Message = GlobalConstants.SuccessfulVoteMessage });
        }
    }
}
