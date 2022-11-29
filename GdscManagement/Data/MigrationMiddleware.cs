using Microsoft.EntityFrameworkCore;

namespace GdscManagement.Data;

public static class MigrationMiddleware
{
    /// <summary>
    ///     Migrates the database if --migrate option is passed to dotnet run.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder" /> to add the middleware to.</param>
    /// <returns>A boolean indicating if migration occured or not</returns>
    public static void MigrateIfNeeded(this IApplicationBuilder app)
    {
        if (ShouldMigrate())
        {
            app.Migrate();
        }
    }

    private static void Migrate(this IApplicationBuilder app)
    {
        Console.WriteLine("Applying migrations...");

        using var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
        if (dbContext is not null)
        {
            dbContext.Database.MigrateAsync().Wait();
            Console.WriteLine("Done!");

            return;
        }

        Console.WriteLine("DbContext is null. No migrations run!");
    }

    private static bool ShouldMigrate()
    {
        var args = Environment.GetCommandLineArgs();
        return args.Contains("--migrate") || args.Contains("migrate");
    }
}
