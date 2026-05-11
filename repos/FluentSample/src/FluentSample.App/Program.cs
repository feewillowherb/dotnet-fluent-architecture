using FluentSample.App;
using FluentSample.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Volo.Abp;

var builder = Host.CreateApplicationBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

// Configure ABP application
builder.Services.AddAbp<FluentSampleAppModule>(options =>
{
    // Register ABP modules
});

var host = builder.Build();

// Run the ABP application
await host.RunAbpAsync();
