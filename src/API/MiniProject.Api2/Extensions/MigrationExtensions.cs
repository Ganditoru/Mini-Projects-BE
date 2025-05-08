using Microsoft.EntityFrameworkCore;
using MiniProject.Modules.Attendance.Infrastructure.Database;

namespace MiniProject.Api2.Extensions;

internal static class MigrationExtensions
{
    internal static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        ApplyMigration<AttendanceDbContext>(scope);
    }

    public static void ApplyMigration<TDbContext>(IServiceScope scope)
    where TDbContext : DbContext
    {
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        context.Database.Migrate();
    }
}
