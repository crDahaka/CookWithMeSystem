namespace CookWithMeSystem.Models
{
    using CookWithMeSystem.Common.Constants;

    using System.ComponentModel.DataAnnotations;

    public class Ingredient : BaseEntity
    {
        [StringLength(ValidationConstants.MaxIngredientName)]
        public string Name { get; set; }

        public virtual Recipe Recipe { get; set; }

    }
}
