

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniProject.Modules.Events.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using MiniProject.Modules.Events.Domain.Categories;
using MiniProject.Modules.Events.Domain.Events;
using MiniProject.Modules.Events.Domain.TicketTypes;
using MiniProject.Modules.Events.Infrastructure.Categories;
using MiniProject.Modules.Events.Infrastructure.Events;
using MiniProject.Modules.Events.Infrastructure.TicketTypes;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using MiniProject.Modules.Events.Application.Abstractions;
using MiniProject.Modules.Events.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using FluentValidation;
using MiniProject.Modules.Events.Infrastructure.Interceptors;
using MiniProjects.Common.Messaging.Contracts.gRPC;
using MiniProject.Modules.Events.Infrastructure.gRPC;
using MiniProject.Modules.Events.Presentation.gRPC;
using MiniProject.Modules.Events.Presentation.Saga;
using MiniProject.Modules.Events.Presentation.Signal_R;

namespace MiniProject.Modules.Events.Infrastructure;

public static class EventsModule
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        
        app.MapHub<NotificationHub>("/Signal_R/notifications").RequireCors("AllowAll");

        app.MapControllers();
    }

    public static IServiceCollection AddEventsModule(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services.AddControllersFromPresentationProject();
        services.AddMediatRHandlerAndValidation();

        services.AddSignalR();
        services.AddCors(opts =>
        {
            opts.AddPolicy("AllowAll", p =>
              p.AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials()
               .SetIsOriginAllowed(_ => true)
            );
        });

        services.AddGrpcSettings(configuration);
        services.AddSagaOrchestrator();

        services.AddInfrastructure(configuration);
        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database")!;

        services.AddDapperConnectionFActory(databaseConnectionString);
        services.TryAddSingleton<PublishDomainEventsInterceptor>();

        services.AddDbContext<EventsDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    databaseConnectionString,
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Events))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>()));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<EventsDbContext>());

        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
    }

    private static void AddGrpcSettings(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        string? userServiceUrl = configuration
            .GetSection("MiniProject.API2")
            .GetValue<string>("gRPCUrl");

        services
            .AddGrpcClient<CreateEventAttendanceModuleService.CreateEventAttendanceModuleServiceClient>(o =>
            {
                o.Address = new Uri(userServiceUrl!);
            });

        services.AddScoped<IGrpcCreateEvent, GrpcCreateEvent>();
    }

    private static void AddSagaOrchestrator(
        this IServiceCollection services)
    {
        services.AddScoped<CreateEventInEventModuleService>();
        services.AddScoped<CreateEventInAttendanceModuleService>();

        services.AddScoped<ISagaStep, CreateEventInEventModuleService>();
        services.AddScoped<ISagaStep, CreateEventInAttendanceModuleService>();

        services.AddScoped(sp =>
        {
            var orchestrator = new CreateEventSagaOrchestrator();

            IEnumerable<ISagaStep> steps = sp.GetServices<ISagaStep>();

            foreach (ISagaStep step in steps)
            {
                orchestrator.AddStep(step);
            }

            return orchestrator;
        });
    }

    private static void AddDapperConnectionFActory(this IServiceCollection services, string databaseConnectionString)
    {
        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);

        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
    }

    private static void AddMediatRHandlerAndValidation(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly);

            config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly, includeInternalTypes: true);
    }

    private static void AddControllersFromPresentationProject(this IServiceCollection services)
    {
        services.AddControllers().AddApplicationPart(Presentation.AssemblyReference.Assembly);
    }
}

