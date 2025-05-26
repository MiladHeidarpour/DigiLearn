using AngleSharp;
using CoreModule.Application.CategoryAgg;
using CoreModule.Application.CategoryAgg.Create;
using CoreModule.Application.CourseAgg;
using CoreModule.Application.TeacherAgg;
using CoreModule.Domain.CategoryAgg.DomainServices;
using CoreModule.Domain.CourseAgg.DomainServices;
using CoreModule.Domain.TeacherAgg.DomainServices;
using CoreModule.Facade;
using CoreModule.Infrastructure;
using CoreModule.Query;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace CoreModule.Config;

public static class CoreModuleBootstrapper
{
    public static IServiceCollection InitCoreModule(this IServiceCollection services, IConfiguration config)
    {
        CoreModuleFacadeBootstrapper.RegisterDependency(services);
        CoreModuleInfrastructureBootstrapper.RegisterDependency(services, config);
        CoreModuleQueryBootstrapper.RegisterDependency(services, config);

        services.AddMediatR(typeof(CreateCategoryCommand).Assembly);
        services.AddValidatorsFromAssembly(typeof(CreateCategoryCommand).Assembly);

        services.AddScoped<ICourseDomainService, CourseDomainService>();
        services.AddScoped<ITeacherDomainService, TeacherDomainService>();
        services.AddScoped<ICategoryDomainService, CategoryDomainService>();

        return services;
    }
}