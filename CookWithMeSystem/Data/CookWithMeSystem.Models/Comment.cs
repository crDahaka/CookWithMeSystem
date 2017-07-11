namespace CookWithMeSystem.Models
{

    public class Comment : BaseEntity
    {
        public string Content { get; set; }

        public string AuthorID { get; set; }

        public virtual User Author { get; set; }

        public int RecipeID { get; set; }

        public virtual Recipe Recipe { get; set; }

    }
}
