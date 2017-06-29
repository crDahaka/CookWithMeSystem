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

        void Add(Recipe recipe, string publisherID, ICollection<Ingredient> ingredients, ICollection<Step> steps);

        void Update(Recipe recipe, ICollection<Ingredient> ingredients, ICollection<Step> steps);

        void Delete(int id);
    }
}
