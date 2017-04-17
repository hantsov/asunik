using System.Runtime.CompilerServices;
using AutoMapper;
using Domain;
using Domain.Course;
using Domain.Event;
using Domain.Identity;
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
            CreateMap<Course, CourseDto>();
            CreateMap<CourseMember, CourseMemberDto>();
        }
    }
}