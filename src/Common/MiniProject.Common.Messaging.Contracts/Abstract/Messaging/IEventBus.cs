namespace MiniProject.Common.Messaging.Contracts.Abstract.Messaging;
public interface IEventBus
{
    Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default) where T : IIntegrationEvent;
}
