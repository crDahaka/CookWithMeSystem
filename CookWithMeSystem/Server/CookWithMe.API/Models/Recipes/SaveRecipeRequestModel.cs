namespace CookWithMe.API.Models.Recipes
{

    public class SaveRecipeRequestModel
    {
        public string Title { get; set; }

        public int EstimationTime { get; set; }

        public string Preparation { get; set; }

        public bool IsPrivate { get; set; }
    }
}