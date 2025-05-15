using MediatR;
using MiniProject.Modules.Attendance.Domain.Abstract;

namespace MiniProject.Modules.Attendance.Application.Events.CreateEvent;
public sealed record CreateEventCommand(string Title, string Description, string Location, DateTime StartsAtUtc, DateTime? EndsAtUtc) : IRequest<Result<Guid>>;

