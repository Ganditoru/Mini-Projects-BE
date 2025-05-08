
using MiniProject.Common.Messaging.Contracts.Abstract.Messaging;

namespace MiniProject.Common.Messaging.Contracts.IntegrationEvents;
public sealed class CreateCustomerIntegrationEvent : IntegrationEvent
{
    public CreateCustomerIntegrationEvent(Guid id, DateTime occurredOnUtc, Guid customerId, string name, string email): base(id, occurredOnUtc)
    {
        CustomerId = customerId;
        Email = email;
        Name = name;
    }

    public Guid CustomerId { get; init; }
    public string Email { get; init; }
    public string Name { get; init; }
}
