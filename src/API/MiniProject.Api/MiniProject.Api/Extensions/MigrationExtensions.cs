using Microsoft.EntityFrameworkCore;
using MiniProject.Modules.Authentification.Infrastructure.Database;
using MiniProject.Modules.Events.Infrastructure.Database;

namespace MiniProject.Api.Extensions;

internal static class MigrationExtensions
{
    internal static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        ApplyMigration<AuthentificationDbContext>(scope);
        ApplyMigration<EventsDbContext>(scope);
    }

    public static void ApplyMigration<TDbContext>(IServiceScope scope)
    where TDbContext : DbContext
    {
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        context.Database.Migrate();
    }
}
