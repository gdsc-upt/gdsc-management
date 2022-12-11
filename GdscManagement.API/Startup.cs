using System.Reflection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace GdscManagement.API;

public static class Startup
{
    private static readonly OpenApiInfo SwaggerInfo = new() { Title = "Gdsc Management API", Version = "v1" };

    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", SwaggerInfo);
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });
            options.OperationFilter<SecurityRequirementsOperationFilter>();

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
        services.AddAutoMapper(typeof(ApiMappingProfiles));
        return services;
    }

    public static AuthenticationBuilder AddJwt(this AuthenticationBuilder builder)
    {
        return builder.AddJwtBearer(options =>
        {
            options.IncludeErrorDetails = true;
            options.SaveToken = true;
        });
    }

    public static IApplicationBuilder UseApi(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });

        return app;
    }
}
