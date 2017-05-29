namespace CookWithMeSystem.Data
{
    using CookWithMeSystem.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class CookWithMeSystemDbContext : IdentityDbContext<User>
    {
        public CookWithMeSystemDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static CookWithMeSystemDbContext Create()
        {
            return new CookWithMeSystemDbContext();
        }
    }
}
