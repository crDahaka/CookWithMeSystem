namespace CookWithMeSystem.Models
{
    public class Image : BaseEntity
    {
        public string Path { get; set; }

        public int RecipeID { get; set; }
    }
}