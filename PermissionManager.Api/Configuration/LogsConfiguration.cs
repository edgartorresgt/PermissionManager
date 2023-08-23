using Serilog;

namespace PermissionManager.API.Configuration;

public static class LogsConfiguration
{
    public static void ConfigureLogServices(this IServiceCollection services)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();

        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.SetMinimumLevel(LogLevel.Trace);
            loggingBuilder.AddSerilog();
        });

    }

}