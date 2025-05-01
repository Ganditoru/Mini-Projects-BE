namespace MiniProject.Common.Messaging.Contracts.Abstract.EventBus;

public interface IIntegrationEvent
{
    Guid Id { get; }
    DateTime OccurredOnUtc { get; }
}
