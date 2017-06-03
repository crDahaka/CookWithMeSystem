namespace CookWithMeSystem.Models
{
    public class Vote : BaseEntity
    {
        public string VotedById { get; set; }

        public virtual User VotedBy { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

    }
}
