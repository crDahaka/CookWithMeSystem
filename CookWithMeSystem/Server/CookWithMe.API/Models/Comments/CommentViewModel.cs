namespace CookWithMe.API.Models.Comments
{
    using AutoMapper;
    using CookWithMe.API.Infrastructure;
    using CookWithMeSystem.Models;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string AuthorName { get; set; }

        public string Content { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(m => m.AuthorName, opt => opt.MapFrom(c => c.Author.UserName))
                .ForMember(m => m.Content, opt => opt.MapFrom(c => c.Content))
                .ReverseMap();
        }
    }
}