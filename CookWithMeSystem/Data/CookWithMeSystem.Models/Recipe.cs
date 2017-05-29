namespace CookWithMeSystem.Models
{
    using System;

    using System.ComponentModel.DataAnnotations;

    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [MaxLength(10)]
        public int EstimationTime { get; set; }
        
        public string Preparation { get; set; }

        public string PublisherId { get; set; }

        public virtual User Publisher { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsPrivate { get; set; }
    }
}
