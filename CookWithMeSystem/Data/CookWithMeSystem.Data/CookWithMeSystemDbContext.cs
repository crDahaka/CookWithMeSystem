namespace CookWithMeSystem.Data
{
    using System;
    using System.Data.Entity;
    using CookWithMeSystem.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class CookWithMeSystemDbContext : IdentityDbContext<User>, ICookWithMeSystemDbContext
    {
        public CookWithMeSystemDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Recipe> Recipes { get; set; }

        public virtual IDbSet<Ingredient> Ingredients { get; set; }

        public static CookWithMeSystemDbContext Create()
        {
            return new CookWithMeSystemDbContext();
        }
    }
}
