namespace CookWithMeSystem.Data
{
    using System.Data.Entity;
    using CookWithMeSystem.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class CookWithMeSystemDbContext : IdentityDbContext<User>, ICookWithMeSystemDbContext
    {
        public CookWithMeSystemDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public virtual IDbSet<Recipe> Recipes { get; set; }

        public virtual IDbSet<Ingredient> Ingredients { get; set; }

        public virtual IDbSet<Step> Steps { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<Picture> Pictures { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<Vote> Votes { get; set; }

        public static CookWithMeSystemDbContext Create()
        {
            return new CookWithMeSystemDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>()
                .HasMany<Ingredient>(r => r.Ingredients)
                .WithMany(i => i.Recipes)
                .Map(ri =>
                {
                    ri.MapLeftKey("RecipeID");
                    ri.MapRightKey("IngredientID");
                    ri.ToTable("RecipeIngredients");
                });

            //modelBuilder.Entity<Step>()
            //    .HasRequired<Recipe>(r => r.Recipe)
            //    .WithMany(s => s.Steps)
            //    .HasForeignKey(s => s.RecipeID)
            //    .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);

        }
    }
}
