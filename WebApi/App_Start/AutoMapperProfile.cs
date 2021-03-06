﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AutoMapper;
using Domain;
using Domain.Album;
using Domain.Course;
using Domain.Event;
using Domain.Identity;
using WebApi.Models.Account;
using WebApi.Models.Albums;
using WebApi.Models.Courses;
using WebApi.Models.Events;
using WebApi.Models.Users;

namespace WebApi
{
    // idea from https://github.com/AutoMapper/AutoMapper/issues/1167
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() : base(nameof(AutoMapperProfile))
        {
            CreateMap<User, UserDto>();
            CreateMap<Event, EventDto>();
            CreateMap<EventMember, EventMemberDto>();
            CreateMap<Course, CourseDto>();
            CreateMap<UpdateCourseDto, Course>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Heading, opt => opt.MapFrom(src => src.Heading))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
                .ForMember(dest => dest.ImgLoc, opt => opt.MapFrom(src => src.ImgLoc))
                .ForAllOtherMembers(opt => opt.Ignore());
            CreateMap<CreateCourseDto, Course>()
                .ForMember(dest => dest.Heading, opt => opt.MapFrom(src => src.Heading))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
                .ForMember(dest => dest.ImgLoc, opt => opt.MapFrom(src => src.ImgLoc))
                .ForAllOtherMembers(opt => opt.Ignore());
            CreateMap<CreateEventDto, Event>()
                .ForMember(dest => dest.Heading, opt => opt.MapFrom(src => src.Heading))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForAllOtherMembers(opt => opt.Ignore());
            CreateMap<CourseMember, CoursesCourseMemberDto>();
            CreateMap<CourseMember, UsersCourseMemberDto>();
            CreateMap<Course, UserCourseDto>();
            CreateMap<List<UserRole>, UserRoleDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Select(ur => ur.Role.Name)));
            CreateMap<Album, AlbumDto>();
            CreateMap<AlbumPhoto, AlbumPhotoDto>();
        }
    }
}