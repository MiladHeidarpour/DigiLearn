using CoreModule.Facade.CategoryAgg;
using CoreModule.Facade.CourseAgg;
using CoreModule.Facade.TeacherAgg;
using Microsoft.Extensions.DependencyInjection;

namespace CoreModule.Facade;

public class CoreModuleFacadeBootstrapper
{
    public static void RegisterDependency(IServiceCollection services)
    {
        services.AddScoped<ITeacherFacade, TeacherFacade>();
        services.AddScoped<ICourseCategoryFacade, CourseCategoryFacade>();
        services.AddScoped<ICourseFacade, CourseFacade>();
    }
}