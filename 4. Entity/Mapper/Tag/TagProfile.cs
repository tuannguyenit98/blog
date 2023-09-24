using AutoMapper;
using Common.Extentions;
using DTOs.Blog.Tag;
using Entities.Blog;
using Mapper.Utils;
using System;

namespace Mapper
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<CreateTagDto, Tag>()
                .IgnoreAllNonExisting()
                .ForMember(x => x.CreatedAt, otp => otp.MapFrom(p => DateTime.Now))
                .ForMember(x => x.Slug, otp => otp.MapFrom(p => p.Title.Slugify()));

            CreateMap<UpdateTagDto, Tag>()
                .IgnoreAllNonExisting()
                .ForMember(x => x.UpdatedAt, otp => otp.MapFrom(p => DateTime.Now))
                .ForMember(x => x.Slug, otp => otp.MapFrom(p => p.Title.Slugify()));
        }
    }
}
