

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MiniProject.Common.Messaging.Contracts.gRPC;
using MiniProject.Common.Messaging.Contracts.Http;
using MiniProject.Modules.Attendance.Infrastructure.Database;
using MiniProject.Modules.Attendances.Infrastructure.gRPC;
using MiniProject.Modules.Attendances.Infrastructure.Http;
using MiniProjects.Common.Messaging.Contracts.gRPC;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Microsoft.EntityFrameworkCore.Migrations;
using MiniProject.Modules.Attendance.Domain.Attendances;
using MiniProject.Modules.Attendance.Infrastructure.Attendances;
using MassTransit;
using MiniProject.Modules.Attendance.Application.Abstract;
using MiniProject.Common.Messaging.Contracts.Abstract.Messaging;
using MiniProject.Modules.Attendance.Infrastructure.Messaging;
using MiniProject.Modules.Attendance.Application;
using MiniProject.Modules.Attendance.Application.Abstract.EventBus;
using MiniProject.Modules.Attendance.Infrastructure.Inbox;
using Quartz;
using MiniProject.Modules.Attendance.Infrastructure.Abstract;
using MiniProject.Modules.Attendance.Domain.Events;
using MiniProject.Modules.Attendance.Infrastructure.Events;
using MiniProject.Modules.Attendance.Presentation.gRPC;
using MiniProject.Modules.Attendance.Infrastructure.gRPC;

namespace MiniProject.Modules.Attendance.Infrastructure;

public static class AttendancesModule
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapControllers();
        app.MapGrpcService<CreateEventGrpcAttendanceModuleService>();
    }

    public static IServiceCollection AddAttendanceModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddIntegrationEventHandlers();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        });

        services.AddGrpcSettings(configuration);
        services.AddHttpSettings(configuration);

        services.AddSingleton<IEventBus, EventBus>();

        services.AddInfrastructure(configuration);
        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database");

        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);

        services.TryAddScoped<IDbConnectionFactory, DbConnectionFactory>();

        services.AddDbContext<AttendanceDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    configuration.GetConnectionString("Database"),
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Attendance))
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IAttendeeRepository, AttendeeRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AttendanceDbContext>());

        services.AddControllers().AddApplicationPart(AssemblyReference.Assembly);

        services.AddQuartz();
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
        services.Configure<InboxOptions>(configuration.GetSection("Attendance:Inbox"));
        services.ConfigureOptions<ConfigureProcessInboxJob>();

        services.AddGrpc();
        services.AddScoped<CreateEventGrpcAttendanceModuleService>();
    }

    private static void AddGrpcSettings(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string? miniProjectApi1 = configuration
            .GetSection("MiniProject.API")
            .GetValue<string>("gRPCUrl");

        services
            .AddGrpcClient<UserService.UserServiceClient>(o =>
            {
                o.Address = new Uri(miniProjectApi1!);
            });

        services
            .AddGrpcClient<CreateEventEventModuleService.CreateEventEventModuleServiceClient>(o =>
            {
                o.Address = new Uri(miniProjectApi1!);
            });

        services.AddScoped<IGrpcUserApi, GrpcUserApi>();
        services.AddScoped<IGrpcCreateEvent, GrpcCreateEvent>();
    }

    private static void AddHttpSettings(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string? userApiUrl = configuration.GetValue<string>("MiniProject.API:HttpUrl");
        services
          .AddHttpClient<IHttpUsersApi, HttpUserApi>(c =>
          {
              c.BaseAddress = new Uri(userApiUrl!);
          });
    }


    private static void AddIntegrationEventHandlers(this IServiceCollection services)
    {
        Type[] integrationEventHandlers = [.. AssemblyReference.Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IIntegrationEventHandler)))
            .Where(t => t.IsClass && !t.IsAbstract)];

        foreach (Type integrationEventHandler in integrationEventHandlers)
        {
            services.TryAddScoped(integrationEventHandler);

            Type integrationEvent = integrationEventHandler
                .GetInterfaces()
                .Single(i => i.IsGenericType)
                .GetGenericArguments()
                .Single();

            Type closedIdempotentHandler =
                typeof(IdempotentIntegrationEventHandler<>).MakeGenericType(integrationEvent);

            services.Decorate(integrationEventHandler, closedIdempotentHandler);
        }
    }
}

