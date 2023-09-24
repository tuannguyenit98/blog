using AutoMapper;
using Common.Extentions;
using DTOs.Blog.Post;
using Entities.Blog;
using Mapper.Utils;
using System;

namespace Mapper
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<CreatePostDto, Post>()
                .IgnoreAllNonExisting()
                .ForMember(x => x.CreatedAt, otp => otp.MapFrom(p => DateTime.Now))
                .ForMember(x => x.Slug, otp => otp.MapFrom(p => p.Title.Slugify()));

            CreateMap<UpdatePostDto, Post>()
                .IgnoreAllNonExisting()
                .ForMember(x => x.UpdatedAt, otp => otp.MapFrom(p => DateTime.Now))
                .ForMember(x => x.Slug, otp => otp.MapFrom(p => p.Title.Slugify()));
        }
    }
}
