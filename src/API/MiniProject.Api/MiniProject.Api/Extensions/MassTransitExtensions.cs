using MassTransit;
using MiniProject.Modules.Ticketing.Presentation.Customers;

namespace MiniProject.Api.Extensions;

internal static class MessagingExtensions
{
    public static IServiceCollection AddRabbitMqMessaging(
             this IServiceCollection services,
             IConfiguration configuration)
    {
        string host = configuration["RabbitMQ:Host"]!;
        string user = configuration["RabbitMQ:Username"]!;
        string pass = configuration["RabbitMQ:Password"]!;

        services.AddMassTransit(x =>
        {
            // 1) let the caller register their consumers, sagas, etc.
            x.AddConsumer<UserRegisteredIntegrationEventConsumer>();

            // 2) configure the transport
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(host, "/", h =>
                {
                    h.Username(user);
                    h.Password(pass);
                });

                // 3) let the caller declare their queues/endpoints
                cfg.ReceiveEndpoint("ticketing.user-registered", e =>
                {
                    e.Durable = true;
                    e.AutoDelete = false;
                    e.PrefetchCount = 16;
                    e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));

                    e.ConfigureConsumer<UserRegisteredIntegrationEventConsumer>(context);
                });
            });
        });

        return services;
    }
}
