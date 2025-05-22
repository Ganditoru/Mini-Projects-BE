using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace MiniProject.Api.Extensions;

internal static class KestrelExtensions
{
    public static WebApplicationBuilder AddKestralHttpConfigurations(
         this WebApplicationBuilder builder)
    {

        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(5021, lo =>
            {
                lo.UseHttps();
                lo.Protocols = HttpProtocols.Http1;
            });
            options.ListenAnyIP(5022, lo =>
            {
                lo.Protocols = HttpProtocols.Http2;
            });
            options.ListenAnyIP(5023, lo =>
            {
                lo.Protocols = HttpProtocols.Http1;
            });
        });

        return builder;
    }
}
