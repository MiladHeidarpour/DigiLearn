using AutoMapper;
using UserModule.Core.Commands.Notifications.Create;
using UserModule.Core.Queries.Users.Dtos;
using UserModule.Data.Entities.Notifications;
using UserModule.Data.Entities.Users;

namespace UserModule.Core;

public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<UserNotification, CreateNotificationCommand>().ReverseMap();
    }
}