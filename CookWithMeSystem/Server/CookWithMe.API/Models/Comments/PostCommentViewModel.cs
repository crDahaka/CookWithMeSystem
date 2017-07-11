namespace CookWithMe.API.Models.Comments
{
    using CookWithMe.API.Infrastructure;
    using CookWithMeSystem.Common.Constants;
    using CookWithMeSystem.Models;
    using System.ComponentModel.DataAnnotations;

    public class PostCommentViewModel : IMapFrom<Comment>
    {

        public PostCommentViewModel()
        {
        }

        public PostCommentViewModel(int recipeID)
        {
            this.RecipeID = recipeID;
        }

        public int RecipeID { get; set; }

        
        [StringLength(ValidationConstants.MaxCommentContent, ErrorMessage = ValidationConstants.ValidationErrorMessage, MinimumLength = ValidationConstants.MinCommentContent)]
        public string Content { get; set; }

    }
}