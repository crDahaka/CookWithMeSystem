namespace CookWithMeSystem.Data
{
    using CookWithMeSystem.Models;

    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public interface ICookWithMeSystemDbContext
    {
        IDbSet<Recipe> Recipes { get; set; }

        IDbSet<Ingredient> Ingredients { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<Image> Images { get; set; }

        IDbSet<Category> Categories { get; set; }

        IDbSet<Vote> Votes { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        void Dispose();

        int SaveChanges();
    }
}
