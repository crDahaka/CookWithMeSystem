namespace CookWithMeSystem.Models
{
    public class Vote : BaseEntity
    {
        public string VotedByID { get; set; }

        public virtual User VotedBy { get; set; }

        public int RecipeID { get; set; }

        public virtual Recipe Recipe { get; set; }

    }
}
