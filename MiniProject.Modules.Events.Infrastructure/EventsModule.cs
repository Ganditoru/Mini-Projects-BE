using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Microsoft.Extensions.DependencyInjection.Extensions;
using FluentValidation;
using MiniProject.Modules.Events.Presentation.Events;
using MiniProject.Modules.Events.Application;
using MiniProject.Modules.Events.Infrastructure.Database;
using MiniProject.Modules.Events.Application.Abstraction;
using MiniProject.Modules.Events.Infrastructure.Data;
using MiniProject.Modules.Events.Domain.Events;
using MiniProject.Modules.Events.Infrastructure.Events;

namespace MiniProject.Modules.Events.Infrastructure;

public static class EventsModule
{

    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        EventsEndpoint.MapEndpoints(app);
    }

    public static IServiceCollection AddEventsModule(
        this IServiceCollection services
        , IConfiguration configuration)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        });

        services.AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);

        services.AddInfrastructure(configuration);
        return services;
    }

    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database")!;

        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

        services.AddDbContext<EventsDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    databaseConnectionString,
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Events))
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IEventRepository, EventRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<EventsDbContext>());
    }
}

