using AutoMapper;
using DtoLayer.CommentDtos;
using EntityLayer.Concrete;

namespace ProjectAPI.Mapping.Comments
{
    public class CommentMapper:Profile
    {
        public CommentMapper()
        {
            CreateMap<Comment, FilterCommentDto>().ReverseMap();
        }
    }
}
