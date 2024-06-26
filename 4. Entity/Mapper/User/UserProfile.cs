﻿using AutoMapper;
using Common.Helpers;
using DTOs.Blog.User;
using Entities.Blog;
using Mapper.Utils;
using System;

namespace Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //CreateMap<User, UserDto>()
            //    .IgnoreAllNonExisting()
            //    .ForMember(x => x.UserName, otp => otp.MapFrom(p => p.UserName));

            CreateMap<CreateUserDto, User>()
                .IgnoreAllNonExisting()
                .ForMember(x => x.CreatedAt, otp => otp.MapFrom(p => DateTime.Now))
                .ForMember(x => x.Role, otp => otp.MapFrom(p => "User"))
                .ForMember(x => x.Password, otp => otp.MapFrom(p => LoginHelper.EncryptPassword(p.Password)));
        }
    }
}
