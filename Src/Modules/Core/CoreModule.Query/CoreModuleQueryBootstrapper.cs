using CoreModule.Query._Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreModule.Query;

public class CoreModuleQueryBootstrapper
{
    public static void RegisterDependency(IServiceCollection services,IConfiguration config)
    {
        services.AddMediatR(typeof(QueryContext).Assembly);

        services.AddDbContext<QueryContext>(option =>
        {
            option.UseSqlServer(config.GetConnectionString("CoreModule_Context"));
        });
    }
}