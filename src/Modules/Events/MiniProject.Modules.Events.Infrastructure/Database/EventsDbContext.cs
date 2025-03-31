using MiniProject.Modules.Events.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using MiniProject.Modules.Events.Application.Abstractions;
using MiniProject.Modules.Events.Domain.Categories;
using MiniProject.Modules.Events.Domain.Events;
using MiniProject.Modules.Events.Domain.TicketTypes;
using MiniProject.Modules.Events.Infrastructure.Events;
using MiniProject.Modules.Events.Infrastructure.TicketTypes;

namespace MiniProject.Modules.Events.Infrastructure.Database;
public sealed class EventsDbContext(DbContextOptions<EventsDbContext> options) : DbContext(options), IUnitOfWork
{

    internal DbSet<Event> Events { get; set; }
    internal DbSet<Category> Categories { get; set; }
    internal DbSet<TicketType> TicketTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Events);

        modelBuilder.ApplyConfiguration(new EventConfiguration());
        modelBuilder.ApplyConfiguration(new TicketTypeConfiguration());

        modelBuilder.Entity<Event>()
            .HasIndex(e => e.Title)
            .IsUnique()
            .HasDatabaseName("IX_Event_Title");
    }
}
