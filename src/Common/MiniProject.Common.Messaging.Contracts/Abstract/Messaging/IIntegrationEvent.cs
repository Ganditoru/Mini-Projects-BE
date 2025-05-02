namespace MiniProject.Common.Messaging.Contracts.Abstract.Messaging;

public interface IIntegrationEvent
{
    Guid Id { get; }
    DateTime OccurredOnUtc { get; }
}
