namespace CookWithMe.API
{
    using CookWithMeSystem.Data;
    using System.Data.Entity;

    using CookWithMeSystem.Data.Migrations;

    public class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CookWithMeSystemDbContext, Configuration>());
            CookWithMeSystemDbContext.Create().Database.Initialize(true);
        }
    }
}