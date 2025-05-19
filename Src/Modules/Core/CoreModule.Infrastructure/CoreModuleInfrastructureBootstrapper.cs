using CoreModule.Infrastructure.Persistent;
using CoreModule.Infrastructure.Persistent.CategoryAgg;
using CoreModule.Infrastructure.Persistent.CourseAgg;
using CoreModule.Infrastructure.Persistent.TeacherAgg;
using CoreModules.Domain.CategoryAgg.Repositories;
using CoreModules.Domain.CourseAgg.Repositories;
using CoreModules.Domain.TeacherAgg.Repositories;
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

        services.AddDbContext<CoreModuleEFContext>(option =>
        {
            option.UseSqlServer(config.GetConnectionString("CoreModule_Context"));
        });
    }
}