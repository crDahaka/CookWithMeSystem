namespace CookWithMeSystem.Data
{
    using CookWithMeSystem.Models;
    using System.Data.Entity;

    public interface ICookWithMeSystemData
    {
        DbContext Context { get; }

        IRepository<Recipe> Recipes { get; }

        IRepository<Ingredient> Ingredients { get; }

        IRepository<User> Users { get; }

        void Dispose();

        int SaveChanges();
    }
}