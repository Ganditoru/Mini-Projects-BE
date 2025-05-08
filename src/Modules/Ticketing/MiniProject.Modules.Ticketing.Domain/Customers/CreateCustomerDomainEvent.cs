using MiniProject.Modules.Ticketing.Domain.Abstract;

namespace MiniProject.Modules.Ticketing.Domain.Customers;
public sealed class CreateCustomerDomainEvent(Guid customerId) : DomainEvent
{
    public Guid CustomerId { get; set; } = customerId;
}
