using Microsoft.EntityFrameworkCore;
using MiniProject.Modules.Attendance.Domain.Attendance;
using MiniProject.Modules.Attendance.Domain.Attendances;
using MiniProject.Modules.Attendance.Infrastructure.Database;

namespace MiniProject.Modules.Attendance.Infrastructure.Attendances;
internal sealed class AttendeeRepository(AttendanceDbContext context) : IAttendeeRepository
{
    public async Task<Attendee?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Attendees.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public void Insert(Attendee attendee)
    {
        context.Attendees.Add(attendee);
    }
}
