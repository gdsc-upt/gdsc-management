using GdscManagement.Common.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GdscManagement.Common;

public static class Startup
{
    public static IServiceCollection AddCommon<TContext>(this IServiceCollection services) where TContext: DbContext
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<DbContext>(x => x.GetRequiredService<TContext>());
        return services;
    }
}
