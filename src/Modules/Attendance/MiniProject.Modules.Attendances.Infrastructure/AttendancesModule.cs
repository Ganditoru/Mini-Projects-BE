

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniProject.Modules.Users.PublicAPI;

namespace MiniProject.Modules.Attendances.Infrastructure;

public static class AttendancesModule
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapControllers();
    }

    public static IServiceCollection AddAttendanceModule(
        this IServiceCollection services)
    {
        services.AddControllersFromPresentationProject();
        return services;
    }

    private static void AddControllersFromPresentationProject(this IServiceCollection services)
    {
        services.AddControllers().AddApplicationPart(Presentation.AssemblyReference.Assembly);
    }

}

