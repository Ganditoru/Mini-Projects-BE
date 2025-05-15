
using MassTransit;
using MediatR;
using MiniProject.Common.Messaging.Contracts.IntegrationEvents;
using MiniProject.Modules.Ticketing.Application.Abstract.Messaging;
using MiniProject.Modules.Ticketing.Application.Customers.GetCustomer;
using MiniProject.Modules.Ticketing.Domain.Customers;

namespace MiniProject.Modules.Ticketing.Application.Customers.CreateCustomer;
internal sealed class CreateCustomerDomainEventHandler(ISender sender, IBus bus) : DomainEventHandler<CreateCustomerDomainEvent>
{
    public override async Task Handle(CreateCustomerDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        CustomerResponse customer = await sender.Send(new GetCustomerQuery(domainEvent.CustomerId), cancellationToken);

        if (customer == null)
        {
            return;
        }

        await bus.Publish(new CreateCustomerIntegrationEvent(
            domainEvent.Id,
            domainEvent.OccurredOnUtc,
            customer.Id,
            customer.Name,
            customer.Email
            ), cancellationToken);
    }
}
