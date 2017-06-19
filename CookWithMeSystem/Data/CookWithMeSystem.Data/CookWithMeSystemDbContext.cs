﻿namespace CookWithMeSystem.Data
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

        public virtual IDbSet<Image> Images { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<Vote> Votes { get; set; }

        public static CookWithMeSystemDbContext Create()
        {
            return new CookWithMeSystemDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Recipe>()
            .HasMany(p => p.Ingredients)
            .WithOptional()
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<Recipe>()
            .HasMany(p => p.Steps)
            .WithOptional()
            .WillCascadeOnDelete(true);

        }
    }
}
