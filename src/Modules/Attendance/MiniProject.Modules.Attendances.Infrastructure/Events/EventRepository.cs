
using Microsoft.EntityFrameworkCore;
using MiniProject.Modules.Attendance.Domain.Events;
using MiniProject.Modules.Attendance.Infrastructure.Database;

namespace MiniProject.Modules.Attendance.Infrastructure.Events;
internal sealed class EventRepository(AttendanceDbContext context) : IEventRepository
{
    public void Insert(Event @event) => context.Events.Add(@event);

    public Task<Event?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => context.Events.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public void Delete(Event @event) => context.Events.Remove(@event);
}
