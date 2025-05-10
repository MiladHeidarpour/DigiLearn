using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserModule.Core.Commands.Users.Register;
using UserModule.Core.Services;
using UserModule.Data;

namespace UserModule.Core;

public static class UserBootstrapper
{
    public static IServiceCollection InitUserModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<UserContext>(option =>
        {
            option.UseSqlServer(config.GetConnectionString("User_Context"));
        });

        services.AddMediatR(typeof(UserBootstrapper).Assembly);
        services.AddScoped<IUserFacade, UserFacade>();
        services.AddScoped<INotificationFacade, NotificationFacade>();
        services.AddValidatorsFromAssembly(typeof(RegisterUserCommandValidator).Assembly);
        services.AddAutoMapper(typeof(MapperProfile).Assembly);

        return services;
    }
}