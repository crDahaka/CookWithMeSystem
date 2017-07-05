namespace CookWithMeSystem.Models
{
    public class Picture : BaseEntity
    {
        public string Path { get; set; }

        public int RecipeID { get; set; }
    }
}