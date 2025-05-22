using Serilog;

namespace MiniProject.Api.Extensions;

internal static class LoggingSerilogExtensions
{

    public static WebApplicationBuilder AddLoggingWithSerilog(
        this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

        return builder;
    }
}
