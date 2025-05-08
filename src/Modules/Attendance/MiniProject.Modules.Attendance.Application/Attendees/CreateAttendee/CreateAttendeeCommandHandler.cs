
using MediatR;
using MiniProject.Modules.Attendance.Application.Abstract;
using MiniProject.Modules.Attendance.Domain.Attendance;
using MiniProject.Modules.Attendance.Domain.Attendances;

namespace MiniProject.Modules.Attendance.Application.Attendees.CreateAttendee;
internal sealed class CreateAttendeeCommandHandler(IAttendeeRepository attendeeRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateAttendeeCommand, AttendeeResponse>
{
    public async Task<AttendeeResponse> Handle(CreateAttendeeCommand request, CancellationToken cancellationToken)
    {
        var attendee = Attendee.Create(request.AttendeeId, request.Email, request.Name);

        attendeeRepository.Insert(attendee);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new AttendeeResponse(attendee.Id, attendee.Email, attendee.Name);
    }
}
