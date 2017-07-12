namespace CookWithMeSystem.Data
{
    using CookWithMeSystem.Models;
    using System.Data.Entity;

    public interface ICookWithMeSystemData
    {
        DbContext Context { get; }

        IRepository<Recipe> Recipes { get; }

        IRepository<Ingredient> Ingredients { get; }

        IRepository<Step> Steps { get; }

        IRepository<Category> Categories { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Picture> Pictures { get; }

        IRepository<Vote> Votes { get; }

        IRepository<User> Users { get; }

        void Dispose();

        int SaveChanges();
    }
}
