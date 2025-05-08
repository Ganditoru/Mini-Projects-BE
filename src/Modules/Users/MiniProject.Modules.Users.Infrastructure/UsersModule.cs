using Microsoft.AspNetCore.Routing;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniProject.Modules.Users.Application.Abstraction;
using MiniProject.Modules.Users.Domain.Users;
using MiniProject.Modules.Users.Infrastructure.Database;
using MiniProject.Modules.Users.Infrastructure.Users;
using MiniProject.Modules.Users.Presentation.Users;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using MiniProject.Modules.Events.Application.Abstractions.Data;
using MiniProject.Modules.Events.Infrastructure.Data;
using MiniProject.Modules.Users.Presentation.Notifications;
using MiniProject.Modules.Users.Infrastructure.PublicApi;
using MiniProject.Common.Messaging.Contracts.Abstract.Messaging;
using MiniProject.Modules.Users.Infrastructure.Messaging;
using MiniProject.Modules.Users.Presentation.gRPC;
using Microsoft.AspNetCore.Builder;
using MiniProject.Modules.Users.PublicApi;

namespace MiniProject.Modules.Users.Infrastructure;
public static class UsersModule
{

    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        UserEndpoints.MapEndpoints(app);
        NotificationEndpoints.MapEndpoints(app);
        app.MapGrpcService<UserGrpcService>();
    }

    public static IServiceCollection AddUsersModule(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly);
        });

        services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly, includeInternalTypes: true);

        services.AddSingleton<IEventBus, EventBus>();

        services.AddInfrastructure(configuration);
        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database")!;

        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

        services.AddDbContext<UserDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    databaseConnectionString,
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.User))
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserPublicApi, UserApi>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UserDbContext>());

        services.AddGrpc();
        services.AddScoped<UserGrpcService>();
    }
}
