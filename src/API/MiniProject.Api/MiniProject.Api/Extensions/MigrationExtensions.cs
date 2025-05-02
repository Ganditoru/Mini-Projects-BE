using Microsoft.EntityFrameworkCore;
using MiniProject.Modules.Users.Infrastructure.Database;
using MiniProject.Modules.Events.Infrastructure.Database;
using MiniProject.Modules.Ticketing.Infrastructure.Database;

namespace MiniProject.Api.Extensions;

internal static class MigrationExtensions
{
    internal static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        ApplyMigration<UserDbContext>(scope);
        ApplyMigration<EventsDbContext>(scope);
        ApplyMigration<TicketingDbContext>(scope);
    }

    public static void ApplyMigration<TDbContext>(IServiceScope scope)
    where TDbContext : DbContext
    {
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        context.Database.Migrate();
    }
}
