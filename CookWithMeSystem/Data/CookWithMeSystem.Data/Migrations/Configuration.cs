namespace CookWithMeSystem.Data.Migrations
{
    using CookWithMeSystem.Common.Generator;
    using CookWithMeSystem.Models;

    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<CookWithMeSystemDbContext>
    {
        private IRandomGenerator random;

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "CookWithMeSystem.Data.CookWithMeSystemDbContext";
            this.random = new RandomGenerator();
        }

        protected override void Seed(CookWithMeSystemDbContext context)
        {
            this.SeedCategories(context);
        }

        private void SeedCategories(CookWithMeSystemDbContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }

            for (int i = 0; i < 5; i++)
            {
                var category = new Category
                {
                    Name = this.random.RandomString(3, 25)
                };

                context.Categories.Add(category);
            }

            context.SaveChanges();
            
        }
    }
}
