namespace CookWithMeSystem.Models
{
    public class Image : BaseEntity
    {
        public byte[] Content { get; set; }

        public string FileExtension { get; set; }
    }
}
