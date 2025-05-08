using MiniProject.Common.Messaging.Contracts.Abstract.Messaging;

namespace MiniProject.Common.Messaging.Contracts.IntegrationEvents;

public sealed class UserRegisteredIntegrationEvent : IntegrationEvent
{
    public UserRegisteredIntegrationEvent(Guid id, DateTime occurredOnUtc, Guid userId, string email, string name) : base(id, occurredOnUtc)
    {
        UserId = userId;
        Email = email;
        Name = name;
    }

    public Guid UserId { get; init; }
    public string Email { get; init; }
    public string Name { get; init; }
}
