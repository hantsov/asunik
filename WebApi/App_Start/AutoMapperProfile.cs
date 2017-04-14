using System.Runtime.CompilerServices;
using AutoMapper;
using Domain.Identity;
using WebApi.Models.Users;

namespace WebApi
{
    // idea from https://github.com/AutoMapper/AutoMapper/issues/1167
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() : base(nameof(AutoMapperProfile))
        {
            CreateMap<User, UserDto>();
        }
    }
}