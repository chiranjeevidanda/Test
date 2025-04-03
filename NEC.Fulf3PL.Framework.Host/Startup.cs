using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NEC.Fulf3PL.Framework.Host.Extension;
using NEC.Fulf3PL.Infrastructure;
using Serilog;
using Serilog.Events;
using System.Reflection;

namespace NEC.Fulf3PL.Framework.Host
{
    public static class Startup
    {
        public static IFunctionsHostBuilder Configure(IFunctionsHostBuilder builder)
        {
            //ConfigureService(builder.Services);

            //ConfigureSerilogWithLogAnalytic(builder.Services);
            var configuration = BuildConfiguration(builder.GetContext().ApplicationRootPath);
            builder.Services.AddAppConfiguration(configuration);

            return builder;
        }

        private static void ConfigureService(IServiceCollection service)
        {
            service.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        private static void ConfigureSerilogWithLogAnalytic(IServiceCollection service)
        {
            var assemblyName = Assembly.GetCallingAssembly().GetName()?.Name?.Replace('.', '_');
            var logName = $"{assemblyName}_Log";
            var logWorkspaceId = Environment.GetEnvironmentVariable("LogWorkspaceId");
            var logWorkspaceKey = Environment.GetEnvironmentVariable("LogWorkspaceKey");
            var filepath = !string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME")) ? @"D:\home\LogFiles\Application\log.txt" : "log.txt";
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .MinimumLevel.Override("Worker", LogEventLevel.Error)
                .MinimumLevel.Override("Host", LogEventLevel.Error)
                .MinimumLevel.Override("System", LogEventLevel.Error)
                .MinimumLevel.Override("Function", LogEventLevel.Error)
                .MinimumLevel.Override("Azure.Storage.Blobs", LogEventLevel.Error)
                .MinimumLevel.Override("Azure.Core", LogEventLevel.Error)
                .MinimumLevel.Override("Azure.Messaging.ServiceBus", LogEventLevel.Error)
                .MinimumLevel.Override("DurableTask.AzureStorage", LogEventLevel.Error)
                .MinimumLevel.Override("DurableTask.Core", LogEventLevel.Error)
                .Enrich.WithProperty("Application", System.Reflection.Assembly.GetExecutingAssembly())
                .Enrich.FromLogContext()
                //.WriteTo.DatadogLogs("XXXXXXXXXXX", configuration: new DatadogConfiguration() { Url = "https://http-intake.logs.datadoghq.eu" }, logLevel: LogEventLevel.Debug)
                .WriteTo.Console()
                //.WriteTo.File(filepath, rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Information)
                .WriteTo.File(filepath, rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Information)
                .WriteTo.AzureAnalytics(logWorkspaceId, logWorkspaceKey, logName)
                //.WriteTo.ApplicationInsights(GetTelemetryClient("InstrumentationKey=065cb9ec-312a-45cc-b9a6-ed3b7b15dc8c;IngestionEndpoint=https://eastus-8.in.applicationinsights.azure.com/;LiveEndpoint=https://eastus.livediagnostics.monitor.azure.com/")
                .CreateLogger();

            service.AddLogging(lb =>
            {
                //lb.ClearProviders(); //--> if used nothing works...
                lb.AddSerilog(Log.Logger, true);
            });
            //var logWorkspaceId = "";
            //var logWorkspaceKey = "";
            //Log.Logger = new LoggerConfiguration()
            //    .WriteTo.AzureAnalytics(logWorkspaceId, logWorkspaceKey)
            //    .CreateLogger();

            //service.AddLogging(lb =>
            //{
            //    lb.AddSerilog(Log.Logger, true);
            //});
        }
        public static IConfiguration BuildConfiguration(string applicationRootPath)
        {
            var config =
                new ConfigurationBuilder()
                    .SetBasePath(applicationRootPath)
                    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile("settings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();

            return config;
        }
    }
}
