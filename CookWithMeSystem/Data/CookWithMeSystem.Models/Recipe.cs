namespace CookWithMeSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Recipe
    {
        private ICollection<Ingredient> ingredients;
        
        public int Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }
        
        public int EstimationTime { get; set; }
        
        public string Preparation { get; set; }

        public string PublisherId { get; set; }

        public virtual User Publisher { get; set; }

        public DateTime? CreationDate { get; set; }

        public bool IsPrivate { get; set; }

        public Recipe()
        {
            this.ingredients = new HashSet<Ingredient>();
        }

        public ICollection<Ingredient> Ingredients
        {
            get { return this.ingredients; }
            set { this.ingredients = value; }
        }

    }
}
