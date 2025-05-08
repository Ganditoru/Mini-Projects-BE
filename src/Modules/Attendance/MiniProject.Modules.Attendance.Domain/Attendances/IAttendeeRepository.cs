using MiniProject.Modules.Attendance.Domain.Attendance;

namespace MiniProject.Modules.Attendance.Domain.Attendances;
public interface IAttendeeRepository
{
    Task<Attendee?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Attendee attendee);
}
