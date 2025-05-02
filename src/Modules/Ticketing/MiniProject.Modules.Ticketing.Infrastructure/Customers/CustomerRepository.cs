using Microsoft.EntityFrameworkCore;
using MiniProject.Modules.Ticketing.Domain.Customers;
using MiniProject.Modules.Ticketing.Infrastructure.Database;

namespace MiniProject.Modules.Ticketing.Infrastructure.Customers;
internal sealed class CustomerRepository(TicketingDbContext context) : ICustomerRepository
{
    public async Task<Customer?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Customers.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public void Insert(Customer customer)
    {
        context.Customers.Add(customer);
    }
}
