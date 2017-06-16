namespace CookWithMeSystem.Services.Contracts
{
    using CookWithMeSystem.Common.Constants;
    using CookWithMeSystem.Models;
    using System.Collections.Generic;
    using System.Linq;

    public interface IRecipeService
    {
        IQueryable<Recipe> All(int page = 1, int pageSize = GlobalConstants.DefaultPageSize);

        void Add(string title, int estimationTime, string preparation, string publisherId, ICollection<Ingredient> ingredients, bool isPrivate = false);
        
    }
}
