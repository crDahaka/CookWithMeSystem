namespace CookWithMeSystem.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using CookWithMeSystem.Models;

    public class CookWithMeSystemData : ICookWithMeSystemData
    {
        private readonly DbContext context;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public CookWithMeSystemData(DbContext context)
        {
            this.context = context;
        }

        public IRepository<Recipe> Recipes
        {
            get { return this.GetRepository<Recipe>(); }
        }

        public IRepository<Ingredient> Ingredients
        {
            get { return this.GetRepository<Ingredient>(); }
        }

        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public DbContext Context
        {
            get { return this.context; }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                }
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(EfGenericRepository<T>);

                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}
