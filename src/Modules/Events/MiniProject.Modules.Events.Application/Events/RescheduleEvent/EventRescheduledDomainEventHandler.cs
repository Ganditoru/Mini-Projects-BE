


using MediatR;
using MiniProject.Modules.Events.Domain.Events;

namespace MiniProject.Modules.Events.Application.Events.RescheduleEvent;
internal sealed class EventRescheduledDomainEventHandler : INotificationHandler<EventRescheduledDomainEvent>
{
    public Task Handle(EventRescheduledDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
