using MediatR;

namespace MiniProject.Modules.Attendance.Application.Attendees.CreateAttendee;
public sealed record CreateAttendeeCommand(Guid AttendeeId, string Email, string Name) : IRequest<AttendeeResponse>;
