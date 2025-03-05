using Microsoft.AspNetCore.Routing;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniProject.Modules.Authentification.Application.Abstraction;
using MiniProject.Modules.Authentification.Domain.Users;
using MiniProject.Modules.Authentification.Infrastructure.Database;
using MiniProject.Modules.Authentification.Infrastructure.Users;
using MiniProject.Modules.Authentification.Presentation.User;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Infrastructure.Data;

namespace MiniProject.Modules.Authentification.Infrastructure;
public static class AuthentificationModule
{

    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        UserEndpoints.MapEndpoints(app);
    }

    public static IServiceCollection AddAuthentificationModule(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly);
        });

        services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly, includeInternalTypes: true);

        services.AddInfrastructure(configuration);
        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database")!;

        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

        services.AddDbContext<AuthentificationDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    databaseConnectionString,
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Authentification))
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AuthentificationDbContext>());

    }
}
