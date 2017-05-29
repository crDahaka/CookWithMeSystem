namespace CookWithMeSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<CookWithMeSystemDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            ContextKey = "CookWithMeSystem.Data.CookWithMeSystemDbContext";
        }

        protected override void Seed(CookWithMeSystemDbContext context)
        {

        }
    }
}
