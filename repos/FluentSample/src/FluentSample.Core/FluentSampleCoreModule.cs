using FluentSample.Core.Configuration;
using FluentSample.Core.Utils;
using FluentSample.Core.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.Modularity;

namespace FluentSample.Core;

/// <summary>
///     Core module for FluentSample.
///     Configures ABP framework integration including EF Core, DI, and services.
/// </summary>
[DependsOn(
    typeof(AbpEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreSqliteModule)
)]
public class FluentSampleCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var services = context.Services;
        var configuration = services.GetConfiguration();

        // Register DbContext with default repositories
        services.AddAbpDbContext<FluentSampleDbContext>(options =>
        {
            options.AddDefaultRepositories(true);
        });

        // Configure SQLite connection
        var connectionString = configuration.GetConnectionString("Default")
                               ?? "Data Source=FluentSample.db";

        // Convert relative path to absolute
        connectionString = DatabaseConnectionStringFactory.FixConnectionString(connectionString);

        services.Configure<AbpDbContextOptions>(options =>
        {
            options.Configure(c =>
            {
                c.DbContextOptions.UseSqlite(connectionString)
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging();
            });
        });

        // Bind application settings
        services.Configure<AppSettings>(
            configuration.GetSection("AppSettings"));
    }
}
