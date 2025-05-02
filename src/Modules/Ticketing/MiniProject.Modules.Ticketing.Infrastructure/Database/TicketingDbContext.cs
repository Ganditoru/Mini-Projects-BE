using Microsoft.EntityFrameworkCore;
using MiniProject.Modules.Ticketing.Application.Abstract;
using MiniProject.Modules.Ticketing.Domain.Customers;
using MiniProject.Modules.Ticketing.Infrastructure.Customers;

namespace MiniProject.Modules.Ticketing.Infrastructure.Database;
public sealed class TicketingDbContext(DbContextOptions<TicketingDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Ticketing);

        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
    }
}
