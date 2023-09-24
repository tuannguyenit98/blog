using AutoMapper;
using Common.Extentions;
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
            CreateMap<CreateCommentDto, Comment>()
                .IgnoreAllNonExisting()
                .ForMember(x => x.CreatedAt, otp => otp.MapFrom(p => DateTime.Now));

            CreateMap<UpdateCommentDto, Comment>()
                .IgnoreAllNonExisting()
                .ForMember(x => x.UpdatedAt, otp => otp.MapFrom(p => DateTime.Now));
        }
    }
}
