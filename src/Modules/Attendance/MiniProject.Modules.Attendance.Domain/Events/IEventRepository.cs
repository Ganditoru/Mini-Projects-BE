
namespace MiniProject.Modules.Attendance.Domain.Events;
public interface IEventRepository
{
    void Insert(Event @event);
    Task<Event?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    void Delete(Event @event);
}
