using Microsoft.EntityFrameworkCore;
using MiniProject.Modules.Authentification.Application.Abstraction;
using MiniProject.Modules.Authentification.Domain.Users;

namespace MiniProject.Modules.Authentification.Infrastructure.Database;

public sealed class AuthentificationDbContext(DbContextOptions<AuthentificationDbContext> options): DbContext(options), IUnitOfWork
{
    internal DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Authentification);
    }
}

