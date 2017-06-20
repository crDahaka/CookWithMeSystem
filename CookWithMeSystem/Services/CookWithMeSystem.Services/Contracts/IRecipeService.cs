namespace CookWithMeSystem.Services.Contracts
{
    using CookWithMeSystem.Common.Constants;
    using CookWithMeSystem.Models;
    using System.Collections.Generic;
    using System.Linq;

    public interface IRecipeService
    {
        IQueryable<Recipe> All(int page = 1, int pageSize = GlobalConstants.DefaultPageSize);

        Recipe GetById(int id);

        void Add(string title, string description, string publisherId, ICollection<Ingredient> ingredients, ICollection<Step> steps, bool isPrivate = false);

        void Update(Recipe recipe);

        void Delete(int id);
    }
}
