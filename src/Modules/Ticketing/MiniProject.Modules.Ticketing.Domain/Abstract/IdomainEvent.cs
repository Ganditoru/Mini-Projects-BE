
using MediatR;

namespace MiniProject.Modules.Ticketing.Domain.Abstract;
public interface IDomainEvent : INotification
{
    Guid Id { get; }

    DateTime OccuredOnUtc { get; }
}
