namespace MiniProject.Common.Messaging.Contracts.Abstract.EventBus;
public interface IEventBus
{
    Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default) where T : IIntegrationEvent;
}
