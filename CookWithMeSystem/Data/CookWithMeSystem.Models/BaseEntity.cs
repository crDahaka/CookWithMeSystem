namespace CookWithMeSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseEntity
    {
        [Key]
        public int ID { get; set; }

    }
}
