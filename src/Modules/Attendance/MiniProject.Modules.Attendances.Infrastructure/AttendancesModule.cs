

using Grpc.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniProject.Common.Messaging.Contracts.User;
using MiniProject.Modules.Attendances.Infrastructure.gRPC;
using MiniProject.Modules.Attendances.Infrastructure.Http;
using MiniProjects.Common.Messaging.Contracts.gRPC;

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
        services.AddGrpcSettings(configuration);
        services.AddHttpSettings(configuration);

        services.AddControllersFromPresentationProject();
        return services;
    }

    private static void AddControllersFromPresentationProject(this IServiceCollection services)
    {
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

