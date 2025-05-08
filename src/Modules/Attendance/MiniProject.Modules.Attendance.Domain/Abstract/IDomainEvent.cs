
using MediatR;

namespace MiniProject.Modules.Attendance.Domain.Abstract;
public interface IDomainEvent : INotification
{
    Guid Id { get; }

    DateTime OccurredOnUtc { get; }
}
