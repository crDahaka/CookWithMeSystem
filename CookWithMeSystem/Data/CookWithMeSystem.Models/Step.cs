namespace CookWithMeSystem.Models
{

    public class Step : BaseEntity
    {
        public string Action { get; set; }

        public int EstimatedTime { get; set; }

        public int? RecipeID { get; set; }

        public virtual Recipe Recipe { get; set; }

    }
}
