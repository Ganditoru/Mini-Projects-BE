using Microsoft.EntityFrameworkCore;
using MiniProject.Modules.Events.Application.Abstraction;
using MiniProject.Modules.Events.Domain.Events;

namespace MiniProject.Modules.Events.Infrastructure.Database;
public sealed class EventsDbContext(DbContextOptions<EventsDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Events);
    }
}
