using MassTransit;
using MiniProject.Common.Messaging.Contracts.Abstract.Messaging;

namespace MiniProject.Modules.Attendance.Infrastructure.Messaging;
public sealed class EventBus(IBus bus) : IEventBus
{
    public async Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default)
        where T : IIntegrationEvent
    {
        await bus.Publish(integrationEvent, cancellationToken);
    }
}
