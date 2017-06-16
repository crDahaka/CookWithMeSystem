namespace CookWithMeSystem.Models
{
    using CookWithMeSystem.Common.Constants;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class User : IdentityUser
    {
        private ICollection<Recipe> recipes;
        private ICollection<Comment> comments;
        
        [StringLength(ValidationConstants.MaxFirstName)]
        public string FirstName { get; set; }
        
        [StringLength(ValidationConstants.MaxLastName)]
        public string LastName { get; set; }

        public bool IsActive { get; set; }

        public User()
        {
            this.recipes = new HashSet<Recipe>();
            this.comments = new HashSet<Comment>();
        }

        public virtual ICollection<Recipe> Recipes
        {
            get { return this.recipes; }
            set { this.recipes = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

            return userIdentity;
        }
    }
}
