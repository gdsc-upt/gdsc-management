using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace GdscManagement.API;

public static class Startup
{
    private static readonly OpenApiInfo SwaggerInfo = new() { Title = "Gdsc Management API", Version = "v1" };

    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen(options => options.SwaggerDoc("v1", SwaggerInfo));
        services.AddAutoMapper(typeof(ApiMappingProfiles));
        return services;
    }

    public static IApplicationBuilder UseApi(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });

        return app;
    }
}
