namespace CookWithMe.API.Controllers
{
    using AutoMapper;
    using CookWithMe.API.Infrastructure.ValidationAttributes;
    using CookWithMe.API.Models;
    using CookWithMe.API.Models.Comments;
    using CookWithMeSystem.Common.Constants;
    using CookWithMeSystem.Data;
    using CookWithMeSystem.Models;
    using Microsoft.AspNet.Identity;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class CommentsController : BaseController
    {
        public CommentsController(ICookWithMeSystemData data)
            :base(data)
        {
        }

        [ValidationModelState]
        [Authorize]
        [Route("api/comments/post")]
        public HttpResponseMessage PostComment([FromBody]PostCommentViewModel comment)
        {
            var dbComment = Mapper.Map<Comment>(comment);
            dbComment.Author = this.UserRecord;
            dbComment.AuthorID = this.User.Identity.GetUserId();

            var recipe = this.Data.Recipes.GetById(comment.RecipeID);
            if (recipe == null)
            {
                return this.CreateSerializedJsonResponse(HttpStatusCode.NotFound, new ErrorViewModel { Message = GlobalConstants.RecipeNotFoundErrorMessage });
            }

            recipe.Comments.Add(dbComment);
            this.Data.SaveChanges();

            return this.CreateSerializedJsonResponse(HttpStatusCode.OK, new { Message = "You have successfully posted a comment." });
        }
    }
}
