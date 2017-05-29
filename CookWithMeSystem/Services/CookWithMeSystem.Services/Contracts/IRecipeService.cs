namespace CookWithMeSystem.Services.Contracts
{
    using CookWithMeSystem.Models;
    using System.Linq;

    public interface IRecipeService
    {
        IQueryable<Recipe> All(int page = 1, int pageSize = 10);

        int Add(string title, int estimationTime, string preparation, bool isPrivate = false);
    }
}
