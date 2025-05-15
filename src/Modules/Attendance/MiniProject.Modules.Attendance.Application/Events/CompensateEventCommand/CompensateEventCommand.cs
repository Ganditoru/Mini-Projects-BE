using MediatR;
using MiniProject.Modules.Attendance.Domain.Abstract;

namespace MiniProject.Modules.Attendance.Application.Events.CompensateEventCommand;
public sealed record CompensateEventCommand(Guid EventId) : IRequest<Result>;
