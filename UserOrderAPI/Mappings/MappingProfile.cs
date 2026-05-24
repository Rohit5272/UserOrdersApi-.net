using AutoMapper;
using UserOrderAPI.DTOs;
using UserOrderAPI.Models;

namespace UserOrderAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponseDto>();

            CreateMap<CreateUserDto, User>();
        }
    }
}
