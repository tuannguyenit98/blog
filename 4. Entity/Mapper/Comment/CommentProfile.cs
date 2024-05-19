using AutoMapper;
using DTOs.Blog.Comment;
using Entities.Blog;
using Mapper.Utils;
using System;

namespace Mapper
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CreateOrUpdateCommentDto, Comment>()
                .IgnoreAllNonExisting()
                .ForMember(x => x.CreatedAt, otp => otp.MapFrom(p => DateTime.Now));
        }
    }
}
