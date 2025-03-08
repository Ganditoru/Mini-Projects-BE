using FluentValidation;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using MiniProject.Modules.Events.Application;
using MiniProject.Modules.Events.Presentation.Events;

namespace MiniProject.Modules.Events.Infrastructure;

public static class EventsModule
{

    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        EventsEndpoint.MapEndpoints(app);
    }

    public static IServiceCollection AddEventsModule(
        this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        });

        services.AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);


        return services;
    }
}

