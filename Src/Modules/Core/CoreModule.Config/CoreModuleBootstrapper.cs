using CoreModule.Application.CategoryAgg.Create;
using CoreModule.Facade;
using CoreModule.Infrastructure;
using CoreModule.Query;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreModule.Config;

public static class CoreModuleBootstrapper
{
    public static IServiceCollection InitCoreModule(this IServiceCollection services, IConfiguration config)
    {
        CoreModuleFacadeBootstrapper.RegisterDependency(services);
        CoreModuleInfrastructureBootstrapper.RegisterDependency(services, config);
        CoreModuleQueryBootstrapper.RegisterDependency(services, config);

        services.AddMediatR(typeof(CreateCategoryCommand).Assembly);
        services.AddValidatorsFromAssembly(typeof(CreateCategoryCommandValidator).Assembly);



        return services;
    }
}