using AutoMapper;
using Common.Extentions;
using DTOs.Blog.Category;
using Entities.Blog;
using Mapper.Utils;
using System;

namespace Mapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryDto, Category>()
                .IgnoreAllNonExisting()
                .ForMember(x => x.CreatedAt, otp => otp.MapFrom(p => DateTime.Now))
                .ForMember(x => x.Slug, otp => otp.MapFrom(p => p.Title.Slugify()));

            CreateMap<UpdateCategoryDto, Category>()
                .IgnoreAllNonExisting()
                .ForMember(x => x.UpdatedAt, otp => otp.MapFrom(p => DateTime.Now))
                .ForMember(x => x.Slug, otp => otp.MapFrom(p => p.Title.Slugify()));
        }
    }
}
