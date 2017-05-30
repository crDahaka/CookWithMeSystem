namespace CookWithMeSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Ingredient
    {
        public int Id { get; set; }

        [StringLength(25)]
        public string Name { get; set; }

        public virtual Recipe Recipe { get; set; }

    }
}
