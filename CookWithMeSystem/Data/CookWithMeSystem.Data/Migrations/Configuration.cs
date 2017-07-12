namespace CookWithMeSystem.Data.Migrations
{
    using CookWithMeSystem.Common;
    using CookWithMeSystem.Common.Constants;
    using CookWithMeSystem.Common.Generator;
    using CookWithMeSystem.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public sealed class Configuration : DbMigrationsConfiguration<CookWithMeSystemDbContext>
    {
        private UserManager<User> userManager;
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
            this.userManager = new UserManager<User>(new UserStore<User>(context));
            this.SeedRoles(context);
            this.SeedUsers(context);
            this.SeedCategoriesWithRecipesWithComments(context);
        }
        
        private void SeedRoles(CookWithMeSystemDbContext context)
        {
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole(GlobalConstants.AdminRole));
            context.SaveChanges();
        }

        private void SeedUsers(CookWithMeSystemDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            for (int i = 0; i < 10; i++)
            {
                var user = new User
                {
                    FirstName = this.random.RandomString(3, 10),
                    LastName = this.random.RandomString(3, 10),
                    Email = string.Format($"{this.random.RandomString(6, 16)}@{this.random.RandomString(3,10)}.com"),
                    UserName = this.random.RandomString(6, 16)
                };
                var result = this.userManager.Create(user, "jaja911");
            }

            var adminUser = new User
            {
                FirstName = "Admin",
                LastName = "AdminAdmin",
                Email = "admin@myApp.com",
                UserName = "Administrator"
            };

            this.userManager.Create(adminUser, GlobalConstants.AdminPassword);
            this.userManager.AddToRole(adminUser.Id, GlobalConstants.AdminRole);
        }

        private void SeedCategoriesWithRecipesWithComments(CookWithMeSystemDbContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }

            var users = context.Users.Take(10).ToList();

            for (int i = 0; i < 5; i++)
            {
                var category = new Category
                {
                    Name = this.random.RandomString(3, 25)
                };

                for (int j = 0; j < 10; j++)
                {
                    var recipe = new Recipe
                    {
                        Title = this.random.RandomString(3, 20),
                        Directions = this.random.RandomString(50, 1000),
                        PreparationTime = (byte)this.random.RandomNumber(1, 249),
                        CookTime = this.random.RandomNumber(1, 249),
                        TotalTime = this.random.RandomNumber(40, 1000),
                        ServingsCount = 3,
                        Level = DifficultyLevel.Intermediate,
                        Publisher = users[this.random.RandomNumber(0, users.Count - 1)],
                        IsPrivate = false
                    };

                    for (int k = 0; k < 5; k++)
                    {
                        var comment = new Comment
                        {
                            Author = users[this.random.RandomNumber(0, users.Count - 1)],
                            Content = this.random.RandomString(100, 200)
                        };

                        recipe.Comments.Add(comment);
                    }

                    category.Recipes.Add(recipe);
                }

                context.Categories.Add(category);
                context.SaveChanges();
            }
            
        }
    }
}
