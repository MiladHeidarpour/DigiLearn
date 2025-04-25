using AutoMapper;
using UserModule.Core.Queries.Users.Dtos;
using UserModule.Data.Entities.Users;

namespace UserModule.Core;

public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<UserDto, User>().ReverseMap();
    }
}