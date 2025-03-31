using Evently.Common.Domain;

namespace MiniProject.Modules.Events.Domain.Events;

public sealed class EventRescheduledDomainEvent(Guid eventId, DateTime startsAtUtc, DateTime? endsAtUtc) : DomainEvent
{
    public Guid EventId { get; set; } = eventId;

    public DateTime StartsAtUtc { get; set; } = startsAtUtc;

    public DateTime? EndsAtUtc { get; set; } = endsAtUtc;
}
