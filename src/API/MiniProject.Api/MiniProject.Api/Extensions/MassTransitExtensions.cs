using MassTransit;

namespace MiniProject.Api.Extensions;

internal static class MessagingExtensions
{
    public static IServiceCollection AddRabbitMqMessaging(
             this IServiceCollection services,
             IConfiguration configuration,
             Action<IBusRegistrationConfigurator>? register = null,
             Action<IRabbitMqBusFactoryConfigurator, IBusRegistrationContext>? configureEndpoints = null)
    {
        string host = configuration["RabbitMQ:Host"]!;
        string user = configuration["RabbitMQ:Username"]!;
        string pass = configuration["RabbitMQ:Password"]!;

        services.AddMassTransit(x =>
        {
            // 1) let the caller register their consumers, sagas, etc.
            register?.Invoke(x);

            // 2) configure the transport
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(host, "/", h =>
                {
                    h.Username(user);
                    h.Password(pass);
                });

                // 3) let the caller declare their queues/endpoints
                configureEndpoints?.Invoke(cfg, context);
            });
        });

        return services;
    }
}
