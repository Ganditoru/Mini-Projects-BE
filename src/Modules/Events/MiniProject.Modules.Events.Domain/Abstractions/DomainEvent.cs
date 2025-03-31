using MiniProject.Modules.Events.Domain.Abstractions;

namespace Evently.Common.Domain;

public abstract class DomainEvent : IDomainEvent
{
    protected DomainEvent()
    {
        Id = Guid.NewGuid();
        OccuredOnUtc = DateTime.UtcNow;
    }

    public Guid Id { get; init; }

    public DateTime OccuredOnUtc { get; init; }
}
