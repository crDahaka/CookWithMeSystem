namespace CookWithMeSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        // public DateTime CreationDate { get; set; }

        // public DateTime UpdateDate { get; set; }

    }
}
