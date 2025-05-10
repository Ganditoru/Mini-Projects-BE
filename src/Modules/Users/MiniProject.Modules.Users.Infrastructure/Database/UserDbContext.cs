using Microsoft.EntityFrameworkCore;
using MiniProject.Modules.Users.Application.Abstraction;
using MiniProject.Modules.Users.Domain.Notifications;
using MiniProject.Modules.Users.Domain.Outbox;
using MiniProject.Modules.Users.Domain.Users;
using MiniProject.Modules.Users.Infrastructure.Outbox;

namespace MiniProject.Modules.Users.Infrastructure.Database;

public sealed class UserDbContext(DbContextOptions<UserDbContext> options): DbContext(options), IUnitOfWork
{
    internal DbSet<User> Users { get; set; }
    internal DbSet<Notification> Notifications { get; set; }
    internal DbSet<OutboxMessage> OutboxMessage { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.User);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.HasMany(x => x.Notifications);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.NotificationDate).IsRequired();
        });
    }
}

