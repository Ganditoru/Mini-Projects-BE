

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

namespace MiniProject.Modules.Attendances.Infrastructure;

public static class AttendancesModule
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapControllers();
    }

    public static IServiceCollection AddAttendanceModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
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

        services.AddDbContext<AttendanceDbContext>((sp, options) =>
    options
        .UseNpgsql(
            configuration.GetConnectionString("Database"),
            npgsqlOptions => npgsqlOptions
                .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Attendance))
        .UseSnakeCaseNamingConvention());

        services.AddScoped<IAttendeeRepository, AttendeeRepository>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AttendanceDbContext>());

        services.AddControllers().AddApplicationPart(Presentation.AssemblyReference.Assembly);
    }

    private static void AddGrpcSettings(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string? userServiceUrl = configuration
            .GetSection("UserService")
            .GetValue<string>("gRPCUrl");

        services
            .AddGrpcClient<UserService.UserServiceClient>(o =>
            {
                o.Address = new Uri(userServiceUrl!);
            });

        services.AddScoped<IGrpcUserApi, GrpcUserApi>();
    }

    private static void AddHttpSettings(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string? userApiUrl = configuration.GetValue<string>("UserService:HttpUrl");
        services
          .AddHttpClient<IHttpUsersApi, HttpUserApi>(c =>
          {
              c.BaseAddress = new Uri(userApiUrl!);
          });
    }

}

