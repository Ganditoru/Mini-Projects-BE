using Microsoft.EntityFrameworkCore;
using MiniProject.Modules.Users.Application.Abstraction;
using MiniProject.Modules.Users.Domain.Notifications;
using MiniProject.Modules.Users.Domain.Users;

namespace MiniProject.Modules.Users.Infrastructure.Database;

public sealed class UserDbContext(DbContextOptions<UserDbContext> options): DbContext(options), IUnitOfWork
{
    internal DbSet<User> Users { get; set; }
    internal DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.User);

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

