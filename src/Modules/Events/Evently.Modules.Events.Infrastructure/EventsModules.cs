using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using FluentValidation;
using Evently.Modules.Events.Presentation.Events;
using Evently.Modules.Events.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Evently.Modules.Events.Application.Abstract;
using Evently.Modules.Events.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;

namespace Evently.Modules.Events.Infrastructure;

public static class EventsModules
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        // Add all endpoints
        EventEndpoints.MapEndpoints(app);
    }

    public static IServiceCollection AddEventsModule(this IServiceCollection services, IConfiguration configuration)
    {

        // Recunoastere handels + Validators
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(Application.AssemblyReference.Assembly);
        });
        services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly, includeInternalTypes: true);

        // Add infrastructure
        services.AddInfrastructure(configuration);
        return services;
    }


    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database")!;

        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);



        // dapper create connectionFactory
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

        // Set-up db
        services.AddDbContext<EventsDbContext>(options =>
            options
                .UseNpgsql(
                    databaseConnectionString,
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Events))
                .UseSnakeCaseNamingConvention());

        // Register repositoryes

        // Register IUnitOFWork
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<EventsDbContext>());

    }

}

