using FluentSample.Core;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace FluentSample.App;

/// <summary>
///     Main application module.
///     Depends on FluentSampleCoreModule for core business logic.
/// </summary>
[DependsOn(
    typeof(FluentSampleCoreModule),
    typeof(AbpAutofacModule)
)]
public class FluentSampleAppModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // Application-level service configuration
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var logger = context.ServiceProvider.GetRequiredService<ILogger<FluentSampleAppModule>>();
        logger.LogInformation("FluentSample application started");
    }
}
