namespace CookWithMeSystem.Models
{
    using CookWithMeSystem.Common.Constants;

    using System.ComponentModel.DataAnnotations;

    public class Comment : BaseEntity
    {
        [StringLength(ValidationConstants.MaxCommentContent)]
        public string Content { get; set; }

        public string AuthorID { get; set; }

        public virtual User Author { get; set; }

        public int RecipeID { get; set; }

        public virtual Recipe Recipe { get; set; }

    }
}
