using CoreModule.Domain.CategoryAgg.Repositories;
using CoreModule.Domain.CourseAgg.Repositories;
using CoreModule.Domain.TeacherAgg.Repositories;
using CoreModule.Infrastructure.EventHandlers;
using CoreModule.Infrastructure.Persistent;
using CoreModule.Infrastructure.Persistent.CategoryAgg;
using CoreModule.Infrastructure.Persistent.CourseAgg;
using CoreModule.Infrastructure.Persistent.TeacherAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreModule.Infrastructure;

public class CoreModuleInfrastructureBootstrapper
{
    public static void RegisterDependency(IServiceCollection services,IConfiguration config)
    {
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<ICourseCategoryRepository, CourseCategoryRepository>();

        services.AddHostedService<UserRegisteredEventHandler>();
        services.AddHostedService<UserEditedEventHandler>();
        services.AddHostedService<UserChangeAvatarEventHandler>();

        services.AddDbContext<CoreModuleEFContext>(option =>
        {
            option.UseSqlServer(config.GetConnectionString("CoreModule_Context"));
        });
    }
}