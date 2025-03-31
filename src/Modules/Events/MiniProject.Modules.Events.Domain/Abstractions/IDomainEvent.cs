using MediatR;

namespace MiniProject.Modules.Events.Domain.Abstractions;

public interface IDomainEvent : INotification
{
    Guid Id { get; }

    DateTime OccuredOnUtc { get; }
}
